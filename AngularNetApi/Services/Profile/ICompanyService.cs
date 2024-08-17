using AngularNetApi.DTOs.User;
using AngularNetApi.Entities;

namespace AngularNetApi.Services.Profile
{
    public interface ICompanyService
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<CreateUserResponse> CreateAsync(CreateUserRequest user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
