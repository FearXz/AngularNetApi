using AngularNetApi.Application.MediatR.ProfileManagement.User.CreateUser;
using AngularNetApi.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt.");
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _mediator.Send(request));
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
    }
}
