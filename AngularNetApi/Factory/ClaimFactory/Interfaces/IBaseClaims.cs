using System.Security.Claims;

namespace AngularNetApi.Factory.ClaimFactory.Interfaces
{
    public interface IBaseClaims
    {
        string Id { get; set; }
        string Role { get; set; }
        string NameId { get; set; }
        List<Claim> GetClaims();
    }
}
