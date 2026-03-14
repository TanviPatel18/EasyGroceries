using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task<List<Order>> GetByCustomerIdAsync(string customerId);
        Task<Order?> GetByIdAsync(string orderId);

        Task<List<Order>> SearchAsync(AdminOrderSearchDto searchDto);
    }
}
