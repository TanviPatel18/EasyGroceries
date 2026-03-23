using ECommerce.Application.Wishlist.DTOs;
using ECommerce.Application.Wishlist.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Wishlist.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;


namespace ECommerce.Application.Wishlist.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _repo;
        private readonly MongoDbContext _db;

        public WishlistService(
            IWishlistRepository repo,
            MongoDbContext db)
        {
            _repo = repo;
            _db = db;
        }

        public async Task<List<WishlistDto>> GetByUserIdAsync(
            string userId)
        {
            var items = await _repo.GetByUserIdAsync(userId);

            var productIds = items.Select(x => x.ProductId).ToList();

            var products = await _db.Products
                .Find(p => productIds.Contains(p.Id))
                .ToListAsync();

            var result = new List<WishlistDto>();

            foreach (var item in items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                if (product != null)
                {
                    result.Add(new WishlistDto
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        OldPrice = product.OldPrice,
                        StockQuantity = product.StockQuantity,
                        ImageUrls = product.ImageUrls ?? new(),
                        AddedOn = item.AddedOn
                    });
                }
            }

            return result;
        }

        public async Task AddAsync(
            string userId, string productId)
        {
            var exists = await _repo.ExistsAsync(
                userId, productId);

            if (exists) return;

            var item = new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
                AddedOn = DateTime.UtcNow
            };

            await _repo.AddAsync(item);
        }

        public async Task RemoveAsync(
            string userId, string productId)
        {
            await _repo.RemoveAsync(userId, productId);
        }

        public async Task<bool> ExistsAsync(
            string userId, string productId)
        {
            return await _repo.ExistsAsync(userId, productId);
        }

        public async Task<int> GetCountAsync(string userId)
        {
            var items = await _repo.GetByUserIdAsync(userId);
            return items.Count;
        }
    }
}
