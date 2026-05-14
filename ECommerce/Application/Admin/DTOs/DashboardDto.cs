using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Admin.DTOs
{
    public class DashboardDto
    {
        public int TotalOrders { get; set; }

        public int PendingOrders { get; set; }

        public int ProcessingOrders { get; set; }

        public int ShippedOrders { get; set; }

        public int DeliveredOrders { get; set; }

        public decimal TotalRevenue { get; set; }
        public List<Order> RecentOrders { get; set; } = new();


    }
}
