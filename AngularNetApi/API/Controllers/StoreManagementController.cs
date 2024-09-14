using System.Security.Claims;
using AngularNetApi.API.Models.StoreManagement;
using AngularNetApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreManagementController : ControllerBase
    {
        private readonly IStoreManagementService _storeSvc;

        public StoreManagementController(IStoreManagementService storeManagementService)
        {
            _storeSvc = storeManagementService;
        }

        [HttpGet("store/{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            return Ok(await _storeSvc.GetStoreByIdAsync(id));
        }

        [HttpPost("createstore")]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid CreateStoreRequest.");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await _storeSvc.CreateStoreAsync(request, userId));
        }

        [HttpPut("updatestore")]
        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreRequest request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid UpdateStoreRequest.");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await _storeSvc.UpdateStoreAsync(request, userId));
        }
    }
}
