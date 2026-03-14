using ECommerce.Application.Sales.DTOs;

namespace ECommerce.Application.Sales.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(string customerId, string productId, int quantity);

        Task<List<CartItemDto>> GetMyCartAsync(string customerId);

        Task RemoveFromCartAsync(string customerId, string productId);

        Task ClearCartAsync(string customerId);
    }
}
