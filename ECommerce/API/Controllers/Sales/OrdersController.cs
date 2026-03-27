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

        private string GetCustomerId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        // ── Place order ──────────────────────────────────────────────────
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderAddressDto addressDto)
        {
            var orderId = await _service.PlaceOrderFromCartAsync(GetCustomerId(), addressDto);
            return Ok(new { message = "Order placed successfully", orderId });
        }

        // ── My orders ────────────────────────────────────────────────────
        [HttpGet("myorders")]
        public async Task<IActionResult> MyOrders()
        {
            var orders = await _service.GetMyOrdersAsync(GetCustomerId());
            return Ok(orders);
        }

        // ── Get single order (customer) ──────────────────────────────────
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(string orderId)
        {
            var order = await _service.GetOrderByIdAsync(GetCustomerId(), orderId);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // ── Get items for an order ───────────────────────────────────────
        [HttpGet("{orderId}/items")]                              // ← NEW
        public async Task<IActionResult> GetOrderItems(string orderId)
        {
            // Verify this order belongs to the current customer
            var order = await _service.GetOrderByIdAsync(GetCustomerId(), orderId);
            if (order == null) return NotFound("Order not found");

            var items = await _service.GetOrderItemsAsync(orderId);
            return Ok(items);
        }

        // ── Cancel order ─────────────────────────────────────────────────
        [HttpDelete("{orderId}/cancel")]                         // ← NEW
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var order = await _service.GetOrderByIdAsync(GetCustomerId(), orderId);
            if (order == null) return NotFound("Order not found");

            if (order.OrderStatus != "Placed")
                return BadRequest("Only orders with status 'Placed' can be cancelled.");

            await _service.UpdateOrderStatusAsync(
                orderId,
                orderStatus: "Cancelled",
                paymentStatus: order.PaymentStatus,
                shippingStatus: "Cancelled");

            return Ok(new { message = "Order cancelled successfully" });
        }
    }

    // Helper DTO for admin status update
  
}