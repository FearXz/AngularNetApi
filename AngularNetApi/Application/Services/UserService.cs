using System.Security.Claims;
using AngularNetApi.API.Models.Profile;
using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Exceptions;
using AngularNetApi.Core.ValueObjects;
using AngularNetApi.DTOs.User;
using AngularNetApi.Infrastructure.Interfaces;
using AngularNetApi.Infrastructure.Persistance;
using AngularNetApi.Infrastructure.Services.Email;
using AngularNetApi.Infrastructure.Services.Email.ViewModels;
using AngularNetApi.Services.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AngularNetApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly EmailTemplate _emailTemplate;
        private readonly LinkGenerator _link;
        private readonly IHttpContextAccessor _http;

        public UserService(
            IMapper mapper,
            ApplicationDbContext db,
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            EmailTemplate emailTemplate,
            LinkGenerator linkGenerator,
            IHttpContextAccessor http
        )
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _emailTemplate = emailTemplate;
            _link = linkGenerator;
            _http = http;
        }

        public async Task<UserProfile> GetByIdAsync(string userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);

                return user;
            }
            catch (Exception ex) when (ex is NotFoundException || ex is ServerErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserService GetByIdAsync", ex);
            }
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
                    var user = _mapper.Map<ApplicationUser>(userRequest);

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
                    userProfile.ApplicationUserId = user.Id;

                    // Add Role to user
                    var addRoleResult = await _userManager.AddToRoleAsync(user, Roles.USER);
                    if (!addRoleResult.Succeeded)
                    {
                        var errors = string.Join(
                            ", ",
                            addRoleResult.Errors.Select(e => e.Description)
                        );
                        throw new BadRequestException($"Error when adding role: {errors}");
                    }

                    // Create claims for ApplicationUser
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Role, Roles.USER),
                        new Claim(ClaimTypes.Name, userProfile.Name)
                    };
                    // Add Claims to ApplicationUser
                    var addClaimsResult = await _userManager.AddClaimsAsync(user, claims);
                    if (!addClaimsResult.Succeeded)
                    {
                        var errors = string.Join(
                            ", ",
                            addClaimsResult.Errors.Select(e => e.Description)
                        );
                        throw new BadRequestException($"Error when adding claims: {errors}");
                    }

                    // Create userProfile
                    var userProfileResult = await _userRepository.CreateAsync(userProfile);

                    // Throw ServerErrorException if userProfile was not created
                    if (userProfileResult == null)
                    {
                        throw new ServerErrorException("Error when adding userProfile");
                    }

                    // Send email to user
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = _link.GetUriByAction(
                        httpContext: _http.HttpContext,
                        action: "ConfirmEmail",
                        controller: "Auth",
                        values: new { userId = user.Id, token }
                    );

                    string HtmlMessage = await _emailTemplate.RenderTemplateAsync(
                        MailT.ConfirmEmailT,
                        new ConfirmEmailVw { ConfirmationLink = confirmationLink }
                    );

                    await _emailSender.SendEmailAsync(
                        user.Email,
                        "Conferma registrazione",
                        HtmlMessage
                    );

                    await transaction.CommitAsync();

                    return new CreateUserResponse { Success = true, NewUserId = user.Id };
                }
                catch (Exception ex) when (ex is BadRequestException || ex is ServerErrorException)
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

        public async Task<UserProfile> UpdateAsync(UserProfile user)
        {
            try
            {
                var existingUser = await _db.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    throw new NotFoundException($"User with ID {user.Id} not found.");
                }

                var updatedProfile = await _userRepository.UpdateAsync(user);

                return updatedProfile;
            }
            catch (Exception ex) when (ex is NotFoundException || ex is ServerErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServerErrorException("Error in UserService UpdateAsync", ex);
            }
        }
    }
}
