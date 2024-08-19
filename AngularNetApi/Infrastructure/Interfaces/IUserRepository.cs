using AngularNetApi.Application.MediatR.ProfileManagement.User;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<UserData> GetByIdAsync(string userId);
        Task<UserData> CreateAsync(UserProfile user);
        Task<UserData> UpdateAsync(UserProfile user);
    }
}
