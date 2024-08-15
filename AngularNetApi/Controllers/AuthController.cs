using AngularNetApi.DTOs.AuthDto;
using AngularNetApi.Entities;
using AngularNetApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserCredentials> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserCredentials> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly IAuthService _authSvc;

        public AuthController(
            UserManager<UserCredentials> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<UserCredentials> signInManager,
            IConfiguration configuration,
            IAuthService authService
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authSvc = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
            try
            {
                LoginResponse result = await _authSvc.Login(login);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest tokens)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid refresh token request.");
                return BadRequest(ModelState);
            }
            try
            {
                RefreshTokenResponse result = await _authSvc.RefreshToken(tokens);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest newUser)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt.");
                return BadRequest(ModelState);
            }
            try
            {
                UserRegisterResponse result = await _authSvc.RegisterUser(newUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddUserRole")]
        public async Task<IActionResult> AddUserRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return Ok("Role added to user");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("CreateRole")]
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
