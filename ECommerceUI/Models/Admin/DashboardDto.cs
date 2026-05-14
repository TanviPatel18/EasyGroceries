using ECommerceUI.Models.Orders;

namespace ECommerceUI.Models.Admin
{
    public class DashboardDto
    {
        public int TotalOrders { get; set; }

        public int PendingOrders { get; set; }

        public int ProcessingOrders { get; set; }

        public int ShippedOrders { get; set; }

        public int DeliveredOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public List<OrderDto> RecentOrders { get; set; } = new();
    }
}
