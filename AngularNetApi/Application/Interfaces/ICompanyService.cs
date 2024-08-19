﻿using AngularNetApi.Application.MediatR.ProfileManagement.User;
using AngularNetApi.Application.MediatR.ProfileManagement.User.CreateUser;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<UserProfile> GetByIdAsync(string userId);
        Task<UserData> CreateAsync(CreateUserRequest user);
        Task<UserProfile> UpdateAsync(UserProfile user);
    }
}
