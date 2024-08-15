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

        public async Task<UserRegistry> AddUserRegistriesAsync(UserRegistry userRegistry)
        {
            try
            {
                await _db.UserRegistries.AddAsync(userRegistry);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userRegistry;
        }

        public async Task<UserRegistry?> GetUserRegistryAsync(UserCredentials user)
        {
            return await _db
                .UserRegistries.Where(x => x.UserCredentialsId == user.Id)
                .Select(x => new UserRegistry
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

        public async Task<CompanyRegistry?> GetCompanyRegistryAsync(UserCredentials user)
        {
            return await _db
                .CompanyRegistries.Where(x => x.UserCredentialsId == user.Id)
                .Select(x => new CompanyRegistry
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
    }
}
