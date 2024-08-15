using AngularNetApi.DTOs.AuthDto;
using Microsoft.AspNetCore.Identity;

namespace AngularNetApi.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> Login(LoginRequest loginRequest);
        public Task<UserRegisterResponse> RegisterUser(UserRegisterRequest registerRequest);
        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public IdentityResult AddUserRole(string userId, string role);
        public IdentityResult CreateRole(string roleName);
    }
}
