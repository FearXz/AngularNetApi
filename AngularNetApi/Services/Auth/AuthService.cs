using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularNetApi.Conext;
using AngularNetApi.DTOs.Auth;
using AngularNetApi.Entities;
using AngularNetApi.Exceptions;
using AngularNetApi.Services.Auth;
using AngularNetApiAngularNetApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AngularNetApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly EUserManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        private const int TokenExpirationTime = 1; // 1 hour

        public AuthService(
            EUserManager userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext db,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _db = db;
            _mapper = mapper;
        }

        public async Task<LoginResponse> Login(LoginRequest login)
        {
            try
            {
                // Attempt to sign in the user
                var result = await _signInManager.PasswordSignInAsync(
                    login.Email,
                    login.Password,
                    true,
                    lockoutOnFailure: false
                );
                // Throw exception if sign in fails
                if (!result.Succeeded)
                {
                    throw new BadRequestException("Invalid credentials");
                }
                // Throw exception if user is locked out
                if (result.IsLockedOut)
                {
                    throw new LockedOutException("User account is locked out.");
                }

                // Get user by email
                var user = await _userManager.FindByEmailAsync(login.Email);

                // Throw exception if user is null
                if (user == null)
                {
                    throw new NotFoundException("User account was not found");
                }

                // Generate access token and refresh token
                var accessToken = GenerateToken(user);
                var refreshToken = GenerateToken(user);

                // Create Identity refresh token object
                var token = new IdentityUserToken<string>
                {
                    UserId = user.Id,
                    LoginProvider = "MyApp",
                    Name = "RefreshToken",
                    Value = refreshToken
                };

                // Set refresh token in user's authentication tokens
                await _userManager.SetAuthenticationTokenAsync(
                    user,
                    token.LoginProvider,
                    token.Name,
                    token.Value
                );
                return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken };
            }
            catch (Exception ex)
                when (ex is BadRequestException
                    || ex is LockedOutException
                    || ex is NotFoundException
                )
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("An unexpected error occurred during login.", ex);
            }
        }

        public async Task<RefreshTokenResponse> RefreshToken(
            RefreshTokenRequest refreshTokenRequest
        )
        {
            try
            {
                var accessTokenUserId = GetIdFromToken(refreshTokenRequest.AccessToken);
                var refreshTokenUserId = GetIdFromToken(refreshTokenRequest.RefreshToken);

                if (refreshTokenUserId == null)
                    throw new Exception("The refresh token has expired or is invalid.");
                if (accessTokenUserId != refreshTokenUserId)
                    throw new Exception(
                        "The user ID in the access token does not match the user ID in the refresh token."
                    );

                var user = _userManager.FindByIdAsync(accessTokenUserId).Result;

                if (user == null)
                    throw new Exception("Invalid client request");

                var storedRefreshToken = _userManager
                    .GetAuthenticationTokenAsync(user, "MyApp", "RefreshToken")
                    .Result;

                if (storedRefreshToken != refreshTokenRequest.RefreshToken)
                    throw new Exception("Invalid refresh token");

                var newAccessToken = GenerateToken(user);
                var newRefreshToken = GenerateToken(user);

                if (newAccessToken == null || newRefreshToken == null)
                    throw new Exception("Error generating tokens");

                var token = new IdentityUserToken<string>
                {
                    UserId = user.Id,
                    LoginProvider = "MyApp",
                    Name = "RefreshToken",
                    Value = newRefreshToken
                };

                var setRefreshTokenResponse = _userManager.SetAuthenticationTokenAsync(
                    user,
                    token.LoginProvider,
                    token.Name,
                    token.Value
                );

                if (!setRefreshTokenResponse.Result.Succeeded)
                    throw new Exception("Error updating refresh token");

                return new RefreshTokenResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        public IdentityResult AddUserRole(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public IdentityResult CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        private string GenerateToken(ApplicationUser user)
        {
            try
            {
                var jwt = _configuration.GetSection("Jwt");
                var key = Encoding.ASCII.GetBytes(jwt["Key"]);
                var claims = _userManager.CreateClaims(user);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(TokenExpirationTime),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                    Issuer = jwt["Issuer"],
                    Audience = jwt["Audience"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }

        private string GetIdFromToken(string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                    ),
                    ValidateLifetime = false // Non validare la scadenza del token
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var principal = tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out SecurityToken securityToken
                );
                var jwtSecurityToken = securityToken as JwtSecurityToken;

                if (
                    jwtSecurityToken == null
                    || !jwtSecurityToken.Header.Alg.Equals(
                        SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase
                    )
                )
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            catch (Exception ex)
            {
                throw new ServerErrorException(ex.Message);
            }
        }
    }
}
