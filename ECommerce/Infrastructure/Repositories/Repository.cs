using ECommerce.Models.Common;
using ECommerce.Models.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ECommerce.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public Repository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync()
            => await _collection.Find(x => !x.IsDeleted).ToListAsync();

        public async Task<T?> GetByIdAsync(string id)
            => await _collection.Find(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();

        public async Task CreateAsync(T entity)
            => await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(T entity)
            => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

        public async Task DeleteAsync(string id)
        {
            var update = Builders<T>.Update.Set(x => x.IsDeleted, true);
            await _collection.UpdateOneAsync(x => x.Id == id, update);
        }
    }
}
