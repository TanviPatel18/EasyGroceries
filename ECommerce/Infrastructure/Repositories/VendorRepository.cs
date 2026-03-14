using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly MongoDbContext _context;

        public VendorRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vendor>> GetAllAsync()
        {
            return await _context.Vendors.Find(v => !v.IsDeleted).ToListAsync();
        }

        public async Task<Vendor> GetByIdAsync(string id)
        {
            return await _context.Vendors.Find(v => v.Id == id && !v.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<Vendor> GetByEmailAsync(string email)
        {
            return await _context.Vendors.Find(v => v.Email == email && !v.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<Vendor> GetByNameAsync(string name)
        {
            return await _context.Vendors
                .Find(v => v.Name == name && !v.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Vendor vendor)
        {
            await _context.Vendors.InsertOneAsync(vendor);
        }

        public async Task UpdateAsync(Vendor vendor)
        {
            vendor.UpdatedOn = DateTime.UtcNow;
            await _context.Vendors.ReplaceOneAsync(v => v.Id == vendor.Id, vendor);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Vendors.UpdateOneAsync(
                v => v.Id == id,
                Builders<Vendor>.Update.Set(v => v.IsDeleted, true).Set(v => v.UpdatedOn, DateTime.UtcNow)
            );
        }

    }
}
