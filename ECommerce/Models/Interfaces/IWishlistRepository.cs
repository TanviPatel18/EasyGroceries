using ECommerce.Models.Wishlist.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IWishlistRepository
    {
        Task<List<WishlistItem>> GetByUserIdAsync(string userId);
        Task AddAsync(WishlistItem item);
        Task RemoveAsync(string userId, string productId);
        Task<bool> ExistsAsync(string userId, string productId);
    }
}
