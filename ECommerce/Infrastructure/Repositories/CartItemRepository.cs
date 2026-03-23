using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly IMongoCollection<ShoppingCartItem> _collection;

        public CartItemRepository(MongoDbContext context)
        {
            _collection = context.ShoppingCartItems;
        }


        public async Task AddItemAsync(ShoppingCartItem item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<List<ShoppingCartItem>> GetItemsByCartIdAsync(string cartId)
        {
            return await _collection
                .Find(x => x.CartId == cartId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task RemoveItemAsync(string cartId, string productId)
        {
            var update = Builders<ShoppingCartItem>
                .Update
                .Set(x => x.IsDeleted, true)
                .Set(x => x.UpdatedOn, DateTime.UtcNow);

            await _collection.UpdateOneAsync(
                x => x.CartId == cartId && x.ProductId == productId,
                update);
        }


        public async Task ClearCartAsync(string cartId)
        {
            var update = Builders<ShoppingCartItem>
                .Update
                .Set(x => x.IsDeleted, true)
                .Set(x => x.UpdatedOn, DateTime.UtcNow);

            await _collection.UpdateManyAsync(
                x => x.CartId == cartId,
                update);
        }

        public async Task UpdateQuantityAsync(
                string cartId, string productId, int quantity)
        {
            var update = Builders<ShoppingCartItem>
                .Update
                .Set(x => x.Quantity, quantity)
                .Set(x => x.UpdatedOn, DateTime.UtcNow);

            await _collection.UpdateOneAsync(
                x => x.CartId == cartId &&
                     x.ProductId == productId &&
                     !x.IsDeleted,
                update);
        }

    }
}
