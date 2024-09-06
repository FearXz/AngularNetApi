using AngularNetApi.API.Models.AccountManagement;
using AngularNetApi.API.Models.Authentication;
using AngularNetApi.Services.Auth;
using AngularNetApi.Services.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authSvc;
        private readonly IAccountService _accountSvc;

        public AuthenticationController(
            RoleManager<IdentityRole> roleManager,
            IAuthService authService,
            IAccountService accountService
        )
        {
            _roleManager = roleManager;
            _authSvc = authService;
            _accountSvc = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

            return Ok(await _authSvc.Login(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt.");
                return BadRequest(ModelState);
            }

            return Ok(await _accountSvc.CreateAsync(request));
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid refresh token request.");
                return BadRequest(ModelState);
            }

            return Ok(await _authSvc.RefreshToken(request));
        }

        [HttpPut("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid email confirmation attempt.");
                return BadRequest(ModelState);
            }

            return Ok(await _authSvc.ConfirmEmailAsync(request));
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name cannot be empty");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return BadRequest("Role already exists");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return Ok("Role created successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}
