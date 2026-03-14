using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Sales
{
    [ApiController]
    [Route("api/admin/orders")]
    //[Authorize(Roles = "Admin")]
    public class AdminOrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public AdminOrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _service.GetOrderByIdAdminAsync(id);
            if (order == null)
                return NotFound();

            var items = await _service.GetOrderItemsAsync(id);  // we will add this

            var dto = new OrderDetailsDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderTotal = order.OrderTotal,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                ShippingStatus = order.ShippingStatus,
                OrderDate = order.OrderDate,
                BillingAddress = order.BillingAddress,
                ShippingAddress = order.ShippingAddress,
                Products = items.Select(x => new OrderItemDto
                {
                    ProductName = x.ProductName,
                    Price = x.UnitPrice,
                    Quantity = x.Quantity
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(
            string id,
            string orderStatus,
            string paymentStatus,
            string shippingStatus)
        {
            await _service.UpdateOrderStatusAsync(id, orderStatus, paymentStatus, shippingStatus);
            return Ok("Order updated");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteOrderAsync(id);
            return Ok("Order deleted");
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] AdminOrderSearchDto dto)
        {
            var result = await _service.SearchOrdersAsync(dto);
            return Ok(result);
        }
    }
}