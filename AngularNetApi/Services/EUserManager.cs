using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AngularNetApi.Conext;
using AngularNetApi.Entities;
using AngularNetApi.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AngularNetApiAngularNetApi.Services
{
    public class EUserManager : UserManager<ApplicationUser>
    {
        private readonly ApplicationDbContext _db;

        public EUserManager(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger,
            ApplicationDbContext db
        )
            : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger
            )
        {
            _db = db;
        }

        //public async Task<UserProfile> AddUserProfileAsync(UserProfile user)
        //{
        //    try
        //    {
        //        await _db.UserProfiles.AddAsync(user);
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return user;
        //}

        public async Task<ProfileBase> GetProfileAsync(string userId)
        {
            try
            {
                var user = await FindByIdAsync(userId);

                if (user == null)
                    throw new Exception("User not found");

                var userRole = await GetRolesAsync(user);

                if (userRole.Count == 0)
                    throw new Exception("User has no role");

                if (userRole[0] == Roles.USER || userRole[0] == Roles.ADMIN)
                {
                    UserProfile UserProfile = await GetUserProfileAsync(user);

                    if (UserProfile == null)
                        throw new Exception("Admin has no profile");

                    return UserProfile;
                }
                else if (userRole[0] == Roles.COMPANY)
                {
                    CompanyProfile companyProfile = await GetCompanyProfileAsync(user);

                    if (companyProfile == null)
                        throw new Exception("Company has no profile");

                    return companyProfile;
                }
                else
                {
                    throw new Exception("Unknown role");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Claim> CreateClaims(ApplicationUser user)
        {
            var userRole = GetRolesAsync(user).Result[0];

            if (string.IsNullOrEmpty(userRole))
                throw new Exception("User has no role");

            if (userRole == Roles.USER || userRole == Roles.ADMIN)
            {
                UserProfile userProfile = (UserProfile)GetProfileAsync(user.Id).Result;
                if (userProfile == null)
                    throw new Exception("User has no registry");

                return new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Name, userProfile.Name)
                };
            }
            else if (userRole == Roles.COMPANY)
            {
                CompanyProfile CompanyProfile = (CompanyProfile)GetProfileAsync(user.Id).Result;
                if (CompanyProfile == null)
                    throw new Exception("Company has no registry");

                return new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, userRole),
                    new Claim(ClaimTypes.Name, CompanyProfile.CompanyName)
                };
            }
            else
            {
                throw new Exception("Unknown role");
            }
        }

        private async Task<UserProfile?> GetUserProfileAsync(ApplicationUser user)
        {
            try
            {
                return await _db
                    .UserProfiles.Where(x => x.ApplicationUserId == user.Id)
                    .Select(x => new UserProfile
                    {
                        Id = x.Id,
                        ApplicationUserId = x.ApplicationUserId,
                        Address = x.Address,
                        City = x.City,
                        CAP = x.CAP,
                        MobileNumber = x.MobileNumber,
                        Name = x.Name,
                        Surname = x.Surname
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<CompanyProfile?> GetCompanyProfileAsync(ApplicationUser user)
        {
            try
            {
                return await _db
                    .CompanyProfiles.Where(x => x.ApplicationUserId == user.Id)
                    .Select(x => new CompanyProfile
                    {
                        Id = x.Id,
                        ApplicationUserId = x.ApplicationUserId,
                        Address = x.Address,
                        City = x.City,
                        CAP = x.CAP,
                        CompanyName = x.CompanyName,
                        VATNumber = x.VATNumber,
                        MobileNumber = x.MobileNumber,
                        TelephoneNumber = x.TelephoneNumber
                    })
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
