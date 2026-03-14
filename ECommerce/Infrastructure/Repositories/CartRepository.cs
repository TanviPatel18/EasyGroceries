using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<ShoppingCart> _collection;

        public CartRepository(MongoDbContext context)
        {
            _collection = context.ShoppingCarts;
        }


        public async Task<ShoppingCart> GetByCustomerIdAsync(string customerId)
        {
            return await _collection
                .Find(x => x.CustomerId == customerId && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task CreateCartAsync(ShoppingCart cart)
        {
            await _collection.InsertOneAsync(cart);
        }

        public async Task UpdateCartAsync(ShoppingCart cart)
        {
            await _collection.ReplaceOneAsync(x => x.Id == cart.Id, cart);
        }
    }
}
