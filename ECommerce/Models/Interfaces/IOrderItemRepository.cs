using ECommerce.Models.Sales.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddItemsAsync(List<OrderItem> items);
        Task<List<OrderItem>> GetByOrderIdAsync(string orderId);
    }
}
