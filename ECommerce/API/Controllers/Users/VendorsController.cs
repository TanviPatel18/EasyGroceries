using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers.Users
{
    [ApiController]
    [Route("api/vendor/orders")]
    [Authorize(Roles = "Vendor")]
    public class VendorOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IShipmentService _shipmentService;

        public VendorOrderController(
            IOrderService orderService,
            IShipmentService shipmentService)
        {
            _orderService = orderService;
            _shipmentService = shipmentService;
        }

        // VendorId = the Customer.Id of the vendor user
        private string GetVendorId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // GET api/vendor/orders
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var orders = await _orderService
                .GetOrdersByVendorAsync(GetVendorId());
            return Ok(orders);
        }

        // PUT api/vendor/orders/{orderId}/status
        // Body: "Processing" or "ReadyToShip"
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateStatus(
            string orderId, [FromBody] string status)
        {
            await _orderService
                .UpdateVendorOrderStatusAsync(orderId, status);
            return Ok(new { message = $"Order marked as {status}" });
        }
    }
}