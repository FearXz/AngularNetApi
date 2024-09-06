using AngularNetApi.API.Models.AccountManagement;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserData> GetByIdAsync(string userId);
        Task<UserData> CreateAsync(UserProfile user);
        Task<UserData> UpdateAsync(UserProfile user);
    }
}
