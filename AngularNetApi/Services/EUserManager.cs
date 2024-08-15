using AngularNetApi.Conext;
using AngularNetApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AngularNetApiAngularNetApi.Services
{
    public class EUserManager : UserManager<UserCredentials>
    {
        private readonly ApplicationDbContext _db;

        public EUserManager(
            IUserStore<UserCredentials> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserCredentials> passwordHasher,
            IEnumerable<IUserValidator<UserCredentials>> userValidators,
            IEnumerable<IPasswordValidator<UserCredentials>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<UserCredentials>> logger,
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

        public async Task<UserProfile> AddUserProfileAsync(UserProfile user)
        {
            try
            {
                await _db.UserProfiles.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }

        public async Task<UserProfile?> GetUserProfileAsync(UserCredentials user)
        {
            try
            {
                return await _db
                    .UserProfiles.Where(x => x.UserCredentialsId == user.Id)
                    .Select(x => new UserProfile
                    {
                        Id = x.Id,
                        UserCredentialsId = x.UserCredentialsId,
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

        public async Task<CompanyProfile?> GetCompanyProfileAsync(UserCredentials user)
        {
            try
            {
                return await _db
                    .CompanyProfiles.Where(x => x.UserCredentialsId == user.Id)
                    .Select(x => new CompanyProfile
                    {
                        Id = x.Id,
                        UserCredentialsId = x.UserCredentialsId,
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
