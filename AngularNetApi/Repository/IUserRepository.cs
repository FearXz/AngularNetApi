using AngularNetApi.Entities;

namespace AngularNetApi.Repository
{
    public interface IUserRepository
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<UserProfile> CreateAsync(UserProfile user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
