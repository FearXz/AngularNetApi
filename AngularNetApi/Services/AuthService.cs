using AngularNetApi.Models;
using AngularNetApi.Services.DbServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngularNetApi.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, UserService userSvc)
        {
            _configuration = configuration;
        }

        public string GenerateToken(LoginResponse user)
        {
            if (user == null)
                return null;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.IdUtente.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var jwt = _configuration.GetSection("Jwt");
            var jwtkey = jwt["Key"];
            var jwtissuer = jwt["Issuer"];
            var jwtaudience = jwt["Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwtissuer,
                jwtaudience,
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
