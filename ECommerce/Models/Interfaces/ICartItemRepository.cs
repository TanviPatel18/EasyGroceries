using ECommerce.Models.Sales.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface ICartItemRepository
    {
        Task AddItemAsync(ShoppingCartItem item);

        Task<List<ShoppingCartItem>> GetItemsByCartIdAsync(string cartId);

        Task RemoveItemAsync(string cartId, string productId);

        Task ClearCartAsync(string cartId);

        Task UpdateQuantityAsync(string cartId, string productId, int quantity);
    }
}
