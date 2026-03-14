using ECommerce.Models.Sales.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface ICartRepository
    {
        Task<ShoppingCart?> GetByCustomerIdAsync(string customerId);
        Task CreateCartAsync(ShoppingCart cart);
        Task UpdateCartAsync(ShoppingCart cart);

    }
}
