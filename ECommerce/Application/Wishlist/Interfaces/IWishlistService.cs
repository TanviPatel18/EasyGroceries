using ECommerce.Application.Wishlist.DTOs;

namespace ECommerce.Application.Wishlist.Interfaces
{
    public interface IWishlistService
    {
        Task<int> GetCountAsync(string userId);
        Task<List<WishlistDto>> GetByUserIdAsync(string userId);
        Task AddAsync(string userId, string productId);
        Task RemoveAsync(string userId, string productId);
        Task<bool> ExistsAsync(string userId, string productId);
    }
}
