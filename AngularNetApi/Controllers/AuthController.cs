using AngularNetApi.DTOs.Auth;
using AngularNetApi.DTOs.User;
using AngularNetApi.Entities;
using AngularNetApi.Exceptions;
using AngularNetApi.Services.Auth;
using AngularNetApi.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AngularNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public readonly IUserService _userSvc;
        private readonly IAuthService _authSvc;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IAuthService authService,
            IUserService userService
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authSvc = authService;
            _userSvc = userService;
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
                return Ok(await _authSvc.Login(login));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message, details = ex.InnerException?.Message });
            }
            catch (LockedOutException ex)
            {
                return StatusCode(
                    423,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (ServerErrorException ex)
            {
                return StatusCode(
                    500,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
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
                return Ok(await _authSvc.RefreshToken(tokens));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message, details = ex.InnerException?.Message });
            }
            catch (SecurityTokenException ex)
            {
                return StatusCode(
                    423,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (ServerErrorException ex)
            {
                return StatusCode(
                    500,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserRequest newUser)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt.");
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _userSvc.CreateAsync(newUser));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (ServerErrorException ex)
            {
                // Gestione delle eccezioni generiche del server
                return StatusCode(
                    500,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
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

            // prima rimuovo tutti i ruoli
            var userRoles = await _userManager.GetRolesAsync(user);
            var resultRemove = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!resultRemove.Succeeded)
            {
                return BadRequest(resultRemove.Errors);
            }

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Role added to user");
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
