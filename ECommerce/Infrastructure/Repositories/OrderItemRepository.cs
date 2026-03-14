using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IMongoCollection<OrderItem> _collection;

        public OrderItemRepository(MongoDbContext context)
        {
            _collection = context.OrderItems;
        }


        public async Task AddItemsAsync(List<OrderItem> items)
        {
            await _collection.InsertManyAsync(items);
        }

        public async Task<List<OrderItem>> GetByOrderIdAsync(string orderId)
        {
            return await _collection
                .Find(x => x.OrderId == orderId && !x.IsDeleted)
                .ToListAsync();
        }
    }
}

