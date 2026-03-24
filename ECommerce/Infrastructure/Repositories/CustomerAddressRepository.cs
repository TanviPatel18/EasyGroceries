using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly MongoDbContext _context;

        public CustomerAddressRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerAddress>> GetByCustomerIdAsync(string customerId)
        {
            return await _context.CustomerAddresses
                .Find(x => x.CustomerId == customerId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<CustomerAddress?> GetByIdAsync(string id)
        {
            return await _context.CustomerAddresses
                .Find(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CustomerAddress address)
        {
            await _context.CustomerAddresses.InsertOneAsync(address);
        }

        public async Task UpdateAsync(CustomerAddress address)
        {
            await _context.CustomerAddresses.ReplaceOneAsync(
                x => x.Id == address.Id,
                address
            );
        }

        public async Task DeleteAsync(string id)
        {
            var update = Builders<CustomerAddress>.Update
                .Set(x => x.IsDeleted, true);

            await _context.CustomerAddresses.UpdateOneAsync(
                x => x.Id == id,
                update
            );
        }
    }
}