using AngularNetApi.Entities;

namespace AngularNetApi.Factory.ClaimFactory.Interfaces
{
    public interface IClaimsFactory
    {
        IBaseClaims CreateClaims(UserCredentials user);
    }
}
