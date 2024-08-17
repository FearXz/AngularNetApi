using AngularNetApi.DTOs.User;
using AngularNetApi.Entities;

namespace AngularNetApi.Services.User
{
    public interface IUserService
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<CreateUserResponse> CreateAsync(CreateUserRequest user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
