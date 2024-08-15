using AngularNetApi.Entities;
using AngularNetApi.Factory.ClaimFactory.Class;
using AngularNetApi.Factory.ClaimFactory.Interfaces;
using AngularNetApi.Util;
using AngularNetApiAngularNetApi.Services;

namespace AngularNetApi.Factory.ClaimFactory
{
    public class ClaimsFactory : IClaimsFactory
    {
        private readonly EUserManager _userManager;

        public ClaimsFactory(EUserManager userManager)
        {
            _userManager = userManager;
        }

        public IBaseClaims CreateClaims(UserCredentials user)
        {
            var userRole = _userManager.GetRolesAsync(user).Result[0];

            if (string.IsNullOrEmpty(userRole))
                throw new Exception("User has no role");

            if (userRole == Roles.USER || userRole == Roles.ADMIN)
            {
                var UserData = _userManager.GetUserRegistryAsync(user).Result;
                if (UserData == null)
                    throw new Exception("User has no registry");

                return new BaseClaims
                {
                    Id = user.Id,
                    Role = userRole,
                    NameId = UserData.Name,
                };
            }
            else if (userRole == Roles.COMPANY)
            {
                var CompanyData = _userManager.GetCompanyRegistryAsync(user).Result;
                if (CompanyData == null)
                    throw new Exception("Company has no registry");

                return new BaseClaims
                {
                    Id = user.Id,
                    Role = userRole,
                    NameId = CompanyData.CompanyName,
                };
            }
            else
            {
                throw new Exception("Unknown role");
            }
        }
    }
}
