using AngularNetApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AngularNetApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessingController : ControllerBase
    {
        private readonly IOrderProcessingService _orderProcessingSvc;

        public OrderProcessingController(IOrderProcessingService orderProcessingSvc)
        {
            _orderProcessingSvc = orderProcessingSvc;
        }

        [HttpGet("stores")]
        public async Task<IActionResult> GetAllStoresAsync()
        {
            return Ok(await _orderProcessingSvc.GetAllStoresAsync());
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _orderProcessingSvc.GetAllCategories());
        }
    }
}
