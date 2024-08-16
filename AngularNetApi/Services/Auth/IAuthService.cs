using AngularNetApi.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace AngularNetApi.Services.Auth
{
    public interface IAuthService
    {
        public Task<LoginResponse> Login(LoginRequest loginRequest);
        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
        public IdentityResult AddUserRole(string userId, string role);
        public IdentityResult CreateRole(string roleName);
    }
}
