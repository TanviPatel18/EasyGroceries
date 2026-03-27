using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Sales.Interfaces
{
    public interface IOrderService
    {
        Task<string> PlaceOrderFromCartAsync(string customerId, OrderAddressDto addressDto); // ← changed
        Task<List<Order>> GetMyOrdersAsync(string customerId);
        Task<Order?> GetOrderByIdAsync(string customerId, string orderId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAdminAsync(string orderId);
        Task<List<Order>> SearchOrdersAsync(AdminOrderSearchDto searchDto);
        Task UpdateOrderStatusAsync(string orderId, string orderStatus, string paymentStatus, string shippingStatus);
        Task DeleteOrderAsync(string orderId);
        Task<List<OrderItem>> GetOrderItemsAsync(string orderId);
    }
}
