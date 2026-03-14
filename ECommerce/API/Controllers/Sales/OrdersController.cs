using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers.Sales
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        private string GetCustomerId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderAddressDto addressDto)
        {
            await _service.PlaceOrderFromCartAsync(GetCustomerId(), addressDto);
            return Ok("Order placed successfully");
        }



        [HttpGet("myorders")]
        public async Task<IActionResult> MyOrders()
        {
            var orders = await _service.GetMyOrdersAsync(GetCustomerId());
            return Ok(orders);
        }
    }
}
