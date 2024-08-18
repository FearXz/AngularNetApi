using AngularNetApi.API.Models.Profile;
using AngularNetApi.Core.Entities;
using AngularNetApi.DTOs.User;

namespace AngularNetApi.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<CreateUserResponse> CreateAsync(CreateUserRequest user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
