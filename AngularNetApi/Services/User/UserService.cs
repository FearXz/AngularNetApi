using AngularNetApi.Conext;
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

        public UserService(IUserRepository userRepository, ApplicationDbContext db, IMapper mapper)
        {
            _userRepository = userRepository;
            _db = db;
            _mapper = mapper;
            _mapper = mapper;
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
                    var user = _mapper.Map<UserCredentials>(userRequest);

                    var createUserResult = await _userManager.CreateAsync(
                        user,
                        userRequest.Password
                    );

                    if (!createUserResult.Succeeded)
                    {
                        var errors = string.Join(
                            ", ",
                            createUserResult.Errors.Select(e => e.Description)
                        );
                        throw new BadRequestException($"Error when creating user: {errors}");
                    }

                    var userProfile = _mapper.Map<UserProfile>(userRequest);
                    userProfile.UserCredentialsId = user.Id;

                    var addRoleResult = await _userManager.AddToRoleAsync(user, Roles.USER);

                    if (!addRoleResult.Succeeded)
                        throw new ServerErrorException("Error adding role to user");

                    var userProfileResult = await _userRepository.CreateAsync(userProfile);

                    if (userProfileResult == null)
                        throw new ServerErrorException("Error when adding userProfile");

                    await transaction.CommitAsync();

                    return new CreateUserResponse { Success = true, NewUserId = user.Id };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public Task<UserProfile> UpdateAsync(UserProfile user)
        {
            throw new NotImplementedException();
        }
    }
}
