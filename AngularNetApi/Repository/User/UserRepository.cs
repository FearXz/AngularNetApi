using AngularNetApi.Conext;
using AngularNetApi.Entities;
using AngularNetApi.Exceptions;

namespace AngularNetApi.Repository.User
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
            try
            {
                var user = await _db.UserProfiles.FindAsync(userId);

                if (user == null)
                {
                    throw new NotFoundException($"User with ID {userId} not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error when finding User");
            }
        }

        public async Task<UserProfile> CreateAsync(UserProfile user)
        {
            try
            {
                var newUser = await _db.UserProfiles.AddAsync(user);
                await _db.SaveChangesAsync();

                return newUser.Entity;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error when creating User");
            }
        }

        public async Task<UserProfile> UpdateAsync(UserProfile user)
        {
            try
            {
                var updatedUser = _db.UserProfiles.Update(user);

                await _db.SaveChangesAsync();

                return updatedUser.Entity;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error when updating User");
            }
        }
    }
}
