using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Interfaces;
using ECommerce.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Category> _collection;

        public CategoryRepository(MongoDbContext context)
        {
            _context = context;
            _collection = context.Categories;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Find(x => !x.IsDeleted).ToListAsync();
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.InsertOneAsync(category);
        }

        public async Task<List<Category>> SearchAsync(CategorySearchDto dto)
        {
            var builder = Builders<Category>.Filter;
            var filter = builder.Eq(x => x.IsDeleted, false); ;

            if (!string.IsNullOrEmpty(dto.Name))
            {
                filter &= builder.Regex(x => x.Name,
                    new MongoDB.Bson.BsonRegularExpression(dto.Name, "i"));
            }

            if (dto.Published.HasValue)
            {
                filter &= builder.Eq(x => x.Published, dto.Published.Value);
            }

            return await _context.Categories.Find(filter).ToListAsync();
        
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _collection
            .Find(c => c.Name == name) // call Find on collection, not context
            .FirstOrDefaultAsync();

        }
        public async Task<Category> GetByIdAsync(string id)
        {
            return await _collection
                .Find(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == category.Id,
                category
            );
        }


    }
}
