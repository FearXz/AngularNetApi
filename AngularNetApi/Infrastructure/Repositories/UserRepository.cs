using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Exceptions;
using AngularNetApi.Infrastructure.Interfaces;
using AngularNetApi.Infrastructure.Persistance;

namespace AngularNetApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UserProfile> GetByIdAsync(string userId)
        {
            var user = await _db.UserProfiles.FindAsync(userId);

            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }
            return user;
        }

        public async Task<UserProfile> CreateAsync(UserProfile user)
        {
            await _db.UserProfiles.AddAsync(user);
            await _db.SaveChangesAsync();
            // ritorna l'utente appena creato
            var newUser = await _db.UserProfiles.FindAsync(user.Id);

            return newUser;
        }

        public async Task<UserProfile> UpdateAsync(UserProfile user)
        {
            _db.UserProfiles.Update(user);
            await _db.SaveChangesAsync();

            var updatedUser = await _db.UserProfiles.FindAsync(user.Id);

            return updatedUser;
        }
    }
}
