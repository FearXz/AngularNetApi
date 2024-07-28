using AngularNetApi.Models;
using AngularNetApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbContext _db;
        private readonly AuthService _authSvc;

        public AuthController(AuthService authService, DbContext db)
        {
            _authSvc = authService;
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginPost loginPost)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                LoginResponse user = _db.userSvc.GetUserByLoginPost(loginPost);

                string token = _authSvc.GenerateToken(user);

                if (user == null || token == null)
                    return Unauthorized();

                user.Token = token;

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
