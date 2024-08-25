using AngularNetApi.API.Models;
using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Exceptions;
using AngularNetApi.Infrastructure.Interfaces;
using AngularNetApi.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _db;

        public AccountRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserData> GetByIdAsync(string userId)
        {
            var user = await _db
                .Users.Where(u => u.Id == userId)
                .Select(u => new UserData
                {
                    Id = u.Id,
                    UserProfileId = u.UserProfile.UserProfileId,
                    Name = u.UserProfile.Name,
                    Surname = u.UserProfile.Surname,
                    Email = u.Email,
                    UserName = u.UserName,
                    Address = u.UserProfile.Address,
                    City = u.UserProfile.City,
                    CAP = u.UserProfile.CAP,
                    PhoneNumber = u.UserProfile.PhoneNumber
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }
            return user;
        }

        public async Task<UserData> CreateAsync(UserProfile user)
        {
            await _db.UserProfiles.AddAsync(user);
            await _db.SaveChangesAsync();
            // ritorna l'utente appena creato
            var newUser = await GetByIdAsync(user.ApplicationUser.Id);

            return newUser;
        }

        public async Task<UserData> UpdateAsync(UserProfile user)
        {
            _db.UserProfiles.Update(user);
            await _db.SaveChangesAsync();

            var updatedUser = await GetByIdAsync(user.ApplicationUser.Id);

            return updatedUser;
        }
    }
}
