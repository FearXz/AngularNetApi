using AngularNetApi.Application.MediatR.Authentication.ConfirmEmail;
using AngularNetApi.Application.MediatR.Authentication.Login;
using AngularNetApi.Application.MediatR.Authentication.RefreshToken;
using AngularNetApi.Core.Entities;
using AngularNetApi.Services.Auth;
using AngularNetApi.Services.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

            return Ok(await _mediator.Send(request));
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid refresh token request.");
                return BadRequest(ModelState);
            }

            return Ok(await _mediator.Send(request));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string Id, string Token)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid email confirmation attempt.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(new ConfirmEmailRequest { Id = Id, Token = Token });
            return Ok("User email confirmed");
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
