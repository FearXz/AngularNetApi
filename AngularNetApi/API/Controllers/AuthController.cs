using AngularNetApi.Application.MediatR.Authentication.Login;
using AngularNetApi.Application.MediatR.Authentication.RefreshToken;
using AngularNetApi.Application.MediatR.ProfileManagement.User.CreateUser;
using AngularNetApi.Core.Entities;
using AngularNetApi.Core.Exceptions;
using AngularNetApi.Services.Auth;
using AngularNetApi.Services.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AngularNetApi.API.Controllers
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
        private readonly IMediator _mediator;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IAuthService authService,
            IUserService userService,
            IMediator mediator
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authSvc = authService;
            _userSvc = userService;
            _mediator = mediator;
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
                return Ok(await _mediator.Send(login));
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

        [HttpPost("refreshtoken")]
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

        [HttpPost("registeruser")]
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

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid email confirmation attempt.");
                return BadRequest(ModelState);
            }
            try
            {
                await _authSvc.ConfirmEmailAsync(userId, token);
                return Ok("User email confirmed");
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new { message = ex.Message, details = ex.InnerException?.Message }
                );
            }
        }
    }
}
