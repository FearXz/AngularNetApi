using System.Security.Claims;

namespace AngularNetApi.Factory.Interfaces
{
    public interface IBaseClaims
    {
        string Id { get; set; }
        string Role { get; set; }
        List<Claim> GetClaims();
    }
}
