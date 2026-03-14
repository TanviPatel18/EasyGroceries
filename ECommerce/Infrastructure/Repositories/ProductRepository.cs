using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Interfaces;
using ECommerce.Persistence;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(MongoDbContext context)
        {
            _products = context.Products;
        }

        public async Task CreateAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _products.ReplaceOneAsync(
                x => x.Id == product.Id,
                product
            );

        }

        public async Task DeleteAsync(string id)
        {
            var update = Builders<Product>.Update.Set("IsDeleted", true);
            await _products.UpdateOneAsync(
                Builders<Product>.Filter.Eq("_id", id),
                update
            );
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var filter = Builders<Product>.Filter.And(
                Builders<Product>.Filter.Eq("_id", new ObjectId(id)),
                Builders<Product>.Filter.Eq("IsDeleted", false)
            );

            return await _products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> SearchAsync(ProductSearchDto search)
        {
            var filter = Builders<Product>.Filter.Eq("IsDeleted", false);

            if (!string.IsNullOrEmpty(search.ProductName))
                filter &= Builders<Product>.Filter.Regex("ProductName", new BsonRegularExpression(search.ProductName, "i"));

            if (!string.IsNullOrEmpty(search.CategoryId))
                filter &= Builders<Product>.Filter.Eq("CategoryId", search.CategoryId);

            if (!string.IsNullOrEmpty(search.ManufacturerId))
                filter &= Builders<Product>.Filter.Eq("ManufacturerId", search.ManufacturerId);

            if (search.ProductType.HasValue)
                filter &= Builders<Product>.Filter.Eq("ProductType", search.ProductType.Value);

            if (search.Published.HasValue)
                filter &= Builders<Product>.Filter.Eq("Published", search.Published);

            if (!string.IsNullOrEmpty(search.SKU))
                filter &= Builders<Product>.Filter.Eq("SKU", search.SKU);

            return await _products.Find(filter).ToListAsync();
        }

    }
}
