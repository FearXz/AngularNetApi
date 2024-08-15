using AngularNetApi.Entities;

namespace AngularNetApi.Factory.Interfaces
{
    public interface IClaimsFactory
    {
        IBaseClaims CreateClaims(UserCredentials user);
    }
}
