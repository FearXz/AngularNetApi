using AngularNetApi.Conext;
using AngularNetApi.Entities;

namespace AngularNetApi.Repository
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
                    throw new Exception("User not found");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserProfile> CreateAsync(UserProfile user)
        {
            try
            {
                var newUser = await _db.UserProfiles.AddAsync(user);

                if (newUser == null)
                {
                    throw new Exception("User not created");
                }
                await _db.SaveChangesAsync();

                return newUser.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserProfile> UpdateAsync(UserProfile user)
        {
            try
            {
                var updatedUser = _db.UserProfiles.Update(user);

                if (updatedUser == null)
                {
                    throw new Exception("User not updated");
                }
                await _db.SaveChangesAsync();

                return updatedUser.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
