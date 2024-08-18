using AngularNetApi.API.Models.Profile;
using AngularNetApi.Core.Entities;
using AngularNetApi.DTOs.User;

namespace AngularNetApi.Services.User
{
    public interface IUserService
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<CreateUserResponse> CreateAsync(CreateUserRequest user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
