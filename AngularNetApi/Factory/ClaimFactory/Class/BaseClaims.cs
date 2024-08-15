using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AngularNetApi.Factory.ClaimFactory.Interfaces;

namespace AngularNetApi.Factory.ClaimFactory.Class
{
    public class BaseClaims : IBaseClaims
    {
        public string Id { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string NameId { get; set; } = string.Empty;

        public List<Claim> GetClaims()
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, Role),
                new Claim(ClaimTypes.Name, NameId)
            };
        }
    }
}
