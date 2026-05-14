using ECommerce.Application.Admin.DTOs;
using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            var dashboard = new DashboardDto
            {
                TotalOrders = orders.Count,

                PendingOrders = orders.Count(x =>
                    x.OrderStatus == "Pending"),

                ProcessingOrders = orders.Count(x =>
                    x.OrderStatus == "Processing"),

                ShippedOrders = orders.Count(x =>
                    x.ShippingStatus == "Shipped"),

                DeliveredOrders = orders.Count(x =>
                    x.ShippingStatus == "Delivered"),

                TotalRevenue = orders.Sum(x =>
                    x.OrderTotal),

                RecentOrders = orders
                    .OrderByDescending(x => x.OrderDate)
                    .Take(10)
                    .ToList()
            };

            return Ok(dashboard);
        }
    }
}