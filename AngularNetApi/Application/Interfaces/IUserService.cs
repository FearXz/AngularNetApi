using AngularNetApi.Application.MediatR.ProfileManagement.User;
using AngularNetApi.Application.MediatR.ProfileManagement.User.CreateUser;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Services.User
{
    public interface IUserService
    {
        Task<UserData> GetByIdAsync(string userId);
        Task<UserData> CreateAsync(CreateUserRequest user);
        Task<UserData> UpdateAsync(UserProfile user);
    }
}
