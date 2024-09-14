using AngularNetApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessingController : ControllerBase
    {
        private readonly IStoreManagementService _storeSvc;

        public OrderProcessingController(IStoreManagementService storeService)
        {
            _storeSvc = storeService;
        }

        [HttpGet("store/{id}")]
        public async Task<IActionResult> GetStoreByIdAsync(int id)
        {
            return Ok(await _storeSvc.GetStoreByIdAsync(id));
        }

        [HttpGet("stores")]
        public async Task<IActionResult> GetAllStoresAsync()
        {
            return Ok(await _storeSvc.GetAllStoresAsync());
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _storeSvc.GetAllCategories());
        }
    }
}
