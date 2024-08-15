using AngularNetApi.Entities;
using AngularNetApi.Factory.Class;
using AngularNetApi.Factory.Interfaces;
using AngularNetApi.Util;
using AngularNetApiAngularNetApi.Services;

namespace AngularNetApi.Factory
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

                return new UserClaims
                {
                    Id = user.Id,
                    Email = user.Email,
                    Address = UserData.Address,
                    City = UserData.City,
                    Cap = UserData.CAP,
                    MobileNumber = UserData.MobileNumber,
                    Role = userRole,
                    Name = UserData.Name,
                    Surname = UserData.Surname
                };
            }
            else if (userRole == Roles.COMPANY)
            {
                var CompanyData = _userManager.GetCompanyRegistryAsync(user).Result;
                if (CompanyData == null)
                    throw new Exception("Company has no registry");

                return new CompanyClaims
                {
                    Id = user.Id,
                    Email = user.Email,
                    Address = CompanyData.Address,
                    City = CompanyData.City,
                    Cap = CompanyData.CAP,
                    MobileNumber = CompanyData.MobileNumber,
                    Role = userRole,
                    CompanyName = CompanyData.CompanyName,
                    VatNumber = CompanyData.VATNumber,
                    TelephoneNumber = CompanyData.TelephoneNumber
                };
            }
            else
            {
                throw new Exception("Unknown role");
            }
        }
    }
}
