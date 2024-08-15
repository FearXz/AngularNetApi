using AngularNetApi.Factory.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AngularNetApi.Factory.Class
{
    public class UserClaims : IUserClaims
    {
        public string Id { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Cap { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;

        public List<Claim> GetClaims()
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, Id),
                new Claim(ClaimTypes.Role, Role),
                new Claim("Email", Email),
                new Claim("Address", Address),
                new Claim("City", City),
                new Claim("CAP", Cap),
                new Claim("Name", Name),
                new Claim("Surname", Surname),
                new Claim("MobileNumber", MobileNumber)
            };
        }
    }
}
