using ECommerce.Models.Interfaces;
using ECommerce.Models.Wishlist.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly MongoDbContext _context;

        public WishlistRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<WishlistItem>> GetByUserIdAsync(
            string userId)
        {
            return await _context.Wishlists
                .Find(w => w.UserId == userId && !w.IsDeleted)
                .SortByDescending(w => w.AddedOn)
                .ToListAsync();
        }

        public async Task AddAsync(WishlistItem item)
        {
            await _context.Wishlists.InsertOneAsync(item);
        }

        public async Task RemoveAsync(
            string userId, string productId)
        {
            await _context.Wishlists.DeleteOneAsync(
                w => w.UserId == userId &&
                     w.ProductId == productId);
        }

        public async Task<bool> ExistsAsync(
            string userId, string productId)
        {
            var count = await _context.Wishlists
                .CountDocumentsAsync(
                    w => w.UserId == userId &&
                         w.ProductId == productId &&
                         !w.IsDeleted);
            return count > 0;
        }
    }
}
