using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Interfaces;
using ECommerce.Persistence;
using ECommerce.Persistence;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text;
using System;

namespace ECommerce.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IMongoCollection<Manufacturer> _collection;
        public ManufacturerRepository(MongoDbContext context)
        {
            _collection = context.Manufacturers;
        }

        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Manufacturer> GetByNameAsync(string name)
        {
            return await _collection
                .Find(x => x.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CreateManufacturerDto dto)
        { 
                var manufacturer = new Manufacturer
                {
                    Name = dto.Name,
                    Published = dto.Published,
                    DisplayOrder = dto.DisplayOrder
                };

             await _collection.InsertOneAsync(manufacturer);

        }

        public async Task UpdateAsync(string id, Manufacturer manufacturer)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, manufacturer);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Manufacturer>> SearchAsync(string name)
        {
            return await _collection
                .Find(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

    }
}
