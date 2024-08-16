using AngularNetApi.Entities;

namespace AngularNetApi.Services
{
    public interface IUserService
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<UserProfile> CreateAsync(UserProfile user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
