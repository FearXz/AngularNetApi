using AngularNetApi.Core.Entities;

namespace AngularNetApi.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<UserProfile> CreateAsync(UserProfile user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
