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
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserRepository GetByIdAsync", ex);
            }
        }

        public async Task<UserProfile> CreateAsync(UserProfile user)
        {
            try
            {
                await _db.UserProfiles.AddAsync(user);
                await _db.SaveChangesAsync();
                // ritorna l'utente appena creato
                var newUser = await _db.UserProfiles.FindAsync(user.Id);

                return newUser;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserRepository CreateAsync", ex);
            }
        }

        public async Task<UserProfile> UpdateAsync(UserProfile user)
        {
            try
            {
                _db.UserProfiles.Update(user);
                await _db.SaveChangesAsync();

                var updatedUser = await _db.UserProfiles.FindAsync(user.Id);

                return updatedUser;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserRepository UpdateAsync", ex);
            }
        }
    }
}
