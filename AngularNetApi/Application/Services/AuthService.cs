﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularNetApi.API.Models.Authentication;
using AngularNetApi.Application.MediatR.Authentication.ConfirmEmail;
using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Exceptions;
using AngularNetApi.Infrastructure.Persistance;
using AngularNetApi.Services.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AngularNetApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        private const int TokenExpirationTime = 1; // 1 hour

        public AuthService(
            UserManager<ApplicationUser> userManager,
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
            // Attempt to sign in the user
            var result = await _signInManager.PasswordSignInAsync(
                login.Email,
                login.Password,
                true,
                lockoutOnFailure: false
            );
            if (result.IsNotAllowed)
            {
                throw new ForbiddenException("User is not allowed to sign in.");
            }
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
            // Get user roles
            var userRoles = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            // Generate access token and refresh token
            var accessToken = await GenerateTokenAsync(user);
            var refreshToken = await GenerateTokenAsync(user);

            // Create Identity refresh token object
            var token = new IdentityUserToken<string>
            {
                UserId = user.Id,
                LoginProvider = "Take2Me",
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

        public async Task<RefreshTokenResponse> RefreshToken(
            RefreshTokenRequest refreshTokenRequest
        )
        {
            // Get user ID from access token and refresh token
            var accessTokenUserId = GetIdFromToken(refreshTokenRequest.AccessToken);
            var refreshTokenUserId = GetIdFromToken(refreshTokenRequest.RefreshToken);

            // Throw exception if refresh token user ID is null
            if (refreshTokenUserId == null)
            {
                throw new BadRequestException("The refresh token has expired or is invalid.");
            }
            // Throw exception if user ID in access token does not match user ID in refresh token
            if (accessTokenUserId != refreshTokenUserId)
            {
                throw new BadRequestException(
                    "The user ID in the access token does not match the user ID in the refresh token."
                );
            }
            // Get user by user ID
            var user = await _userManager.FindByIdAsync(accessTokenUserId);

            // Throw exception if user is null
            if (user == null)
            {
                throw new NotFoundException("User Not Found in AuthService Refreshtoken");
            }
            // Get stored refresh token
            var storedRefreshToken = await _userManager.GetAuthenticationTokenAsync(
                user,
                "Take2Me",
                "RefreshToken"
            );
            // Throw exception if stored refresh token does not match refresh token in request
            if (storedRefreshToken != refreshTokenRequest.RefreshToken)
            {
                throw new BadRequestException("Invalid refresh token");
            }
            // Generate new access token and refresh token
            var newAccessToken = await GenerateTokenAsync(user);
            var newRefreshToken = await GenerateTokenAsync(user);

            // Create Identity refresh token object
            var token = new IdentityUserToken<string>
            {
                UserId = user.Id,
                LoginProvider = "Take2Me",
                Name = "RefreshToken",
                Value = newRefreshToken
            };
            // Set refresh token in user's authentication tokens
            var setRefreshTokenResponse = await _userManager.SetAuthenticationTokenAsync(
                user,
                token.LoginProvider,
                token.Name,
                token.Value
            );

            return new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<ConfirmEmailResponse> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Error when confirming email: {errors}");
            }
            return new ConfirmEmailResponse { Id = request.Id, Email = user.Email };
        }

        public IdentityResult AddUserRole(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public IdentityResult CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var jwt = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwt["Key"]);
            var claims = await _userManager.GetClaimsAsync(user);

            // aggiungo claims jti per generare token univoci
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            if (claims == null)
            {
                throw new NotFoundException("User claims not found");
            }

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

        private string GetIdFromToken(string token)
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
    }
}
