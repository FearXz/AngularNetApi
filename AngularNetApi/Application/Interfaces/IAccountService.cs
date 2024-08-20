using AngularNetApi.API.Models;
using AngularNetApi.Application.MediatR.Authentication.Register;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Services.User
{
    public interface IAccountService
    {
        Task<UserData> GetByIdAsync(string userId);
        Task<UserData> CreateAsync(CreateUserRequest user);
        Task<UserData> UpdateAsync(UserProfile user);
    }
}
