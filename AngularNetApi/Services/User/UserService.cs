﻿using AngularNetApi.Conext;
using AngularNetApi.DTOs.User;
using AngularNetApi.Entities;
using AngularNetApi.Exceptions;
using AngularNetApi.Repository.User;
using AngularNetApi.Util;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AngularNetApi.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<UserCredentials> _userManager;

        public UserService(
            IUserRepository userRepository,
            ApplicationDbContext db,
            IMapper mapper,
            UserManager<UserCredentials> userManager
        )
        {
            _userRepository = userRepository;
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }

        public Task<UserProfile> GetByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserRequest userRequest)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    // Check if user with email already exists
                    var userExists = await _userManager.FindByEmailAsync(userRequest.Email);

                    // If user exists, throw BadRequestException
                    if (userExists != null && userExists.Email == userRequest.Email)
                    {
                        throw new BadRequestException("User with this email already exists");
                    }

                    // Map CreateUserRequest to UserCredentials
                    var user = _mapper.Map<UserCredentials>(userRequest);

                    // Create userCredentials
                    var createUserResult = await _userManager.CreateAsync(
                        user,
                        userRequest.Password
                    );

                    // Throw BadRequestException if user was not created
                    if (!createUserResult.Succeeded)
                    {
                        var errors = string.Join(
                            ", ",
                            createUserResult.Errors.Select(e => e.Description)
                        );
                        throw new BadRequestException($"Error when creating user: {errors}");
                    }

                    // Map CreateUserRequest to UserProfile
                    var userProfile = _mapper.Map<UserProfile>(userRequest);
                    userProfile.UserCredentialsId = user.Id;

                    // Add Role to user
                    var addRoleResult = await _userManager.AddToRoleAsync(user, Roles.USER);

                    // Throw ServerErrorException if user was not added to role
                    if (!addRoleResult.Succeeded)
                    {
                        throw new ServerErrorException("Error adding role to user");
                    }

                    // Create userProfile
                    var userProfileResult = await _userRepository.CreateAsync(userProfile);

                    // Throw ServerErrorException if userProfile was not created
                    if (userProfileResult == null)
                    {
                        throw new ServerErrorException("Error when adding userProfile");
                    }

                    await transaction.CommitAsync();

                    return new CreateUserResponse { Success = true, NewUserId = user.Id };
                }
                catch (BadRequestException ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
                catch (ServerErrorException ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ServerErrorException(
                        "An unexpected error occurred during user creation.",
                        ex
                    );
                }
            }
        }

        public Task<UserProfile> UpdateAsync(UserProfile user)
        {
            throw new NotImplementedException();
        }
    }
}
