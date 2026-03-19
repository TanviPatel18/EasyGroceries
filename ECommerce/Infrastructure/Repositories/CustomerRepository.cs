using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;


namespace ECommerce.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MongoDbContext _context;

        public CustomerRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
            => await _context.Customers.Find(x => !x.IsDeleted).ToListAsync();

        public async Task<Customer> GetByIdAsync(string id)
            => await _context.Customers.Find(x => x.Id == id && !x.IsDeleted)
                                       .FirstOrDefaultAsync();

        public async Task<Customer> GetByEmailAsync(string email)
            => await _context.Customers.Find(x => x.Email == email && !x.IsDeleted)
                                       .FirstOrDefaultAsync();

        public async Task CreateAsync(Customer customer)
            => await _context.Customers.InsertOneAsync(customer);

        public async Task UpdateAsync(Customer customer)
        {
            customer.UpdatedOn = DateTime.UtcNow;
            await _context.Customers.ReplaceOneAsync(x => x.Id == customer.Id, customer);
        }

        public async Task SoftDeleteByEmailAsync(string email)
        {
            var update = Builders<Customer>.Update
                .Set(x => x.IsDeleted, true)
                .Set(x => x.UpdatedOn, DateTime.UtcNow);

            await _context.Customers.UpdateOneAsync(
                x => x.Email == email && !x.IsDeleted,
                update);
        }


        public async Task<Customer> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Customers
                .Find(x => x.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();
        }
        public async Task UpdatePasswordAsync(string email, string newPasswordHash)
        {
            await _context.Customers.UpdateOneAsync(
                c => c.Email == email,
                Builders<Customer>.Update
                    .Set(c => c.PasswordHash, newPasswordHash)
                    .Set(c => c.UpdatedOn, DateTime.UtcNow));
        }
    }
}

