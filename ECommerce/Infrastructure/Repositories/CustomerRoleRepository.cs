using ECommerce.Application.Users.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;



namespace ECommerce.Infrastructure.Repositories
{
    public class CustomerRoleRepository:ICustomerRoleRepository
    {
        private readonly MongoDbContext _context;


        public CustomerRoleRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerRole>> GetAllAsync()
        {
            return await _context.CustomerRoles
                .Find(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<CustomerRole> GetByIdAsync(string id)
        {
            return await _context.CustomerRoles
                .Find(r => r.Id == id && !r.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<CustomerRole> GetByNameAsync(string roleName)
        {
            return await _context.CustomerRoles
                .Find(r => r.RoleName == roleName && !r.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CustomerRole role)
        {
            await _context.CustomerRoles.InsertOneAsync(role);
        }

        public async Task UpdateAsync(CustomerRole role)
        {
            role.UpdatedOn = DateTime.UtcNow;

            await _context.CustomerRoles
                .ReplaceOneAsync(r => r.Id == role.Id, role);
        }

        public async Task DeleteAsync(string id)
        {
            var update = Builders<CustomerRole>.Update
                .Set(r => r.IsDeleted, true)
                .Set(r => r.UpdatedOn, DateTime.UtcNow);

            await _context.CustomerRoles
                .UpdateOneAsync(r => r.Id == id, update);
        }


    }
}
