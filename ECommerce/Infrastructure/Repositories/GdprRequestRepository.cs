using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class GdprRequestRepository : IGdprRequestRepository
    {
        private readonly IMongoCollection<GdprRequestLog> _collection;

        public GdprRequestRepository(MongoDbContext context)
        {
            _collection = context.GdprRequests;
        }
        // Get single GDPR request by ID
        public async Task<GdprRequestLog> GetByIdAsync(string id)
        {
            return await _collection
                .Find(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        // Get all GDPR requests
        public async Task<List<GdprRequestLog>> GetAllAsync()
        {
            return await _collection
                .Find(x => !x.IsDeleted)
                .SortByDescending(x => x.RequestDate)
                .ToListAsync();
        }

        // Create GDPR request
        public async Task CreateAsync(GdprRequestLog entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        // Search GDPR requests with filters
        public async Task<List<GdprRequestLog>> SearchAsync(string email, string requestType)
        {
            var filter = Builders<GdprRequestLog>.Filter.Eq(x => x.IsDeleted, false);

            if (!string.IsNullOrEmpty(email))
                filter &= Builders<GdprRequestLog>.Filter.Regex(
                    x => x.CustomerEmail,
                    new MongoDB.Bson.BsonRegularExpression(email, "i"));

            if (!string.IsNullOrEmpty(requestType) && requestType != "All")
                filter &= Builders<GdprRequestLog>.Filter.Eq(x => x.RequestType, requestType);

            return await _collection
                .Find(filter)
                .SortByDescending(x => x.RequestDate)
                .ToListAsync();
        }

        // Update status of GDPR request
        public async Task UpdateStatusAsync(string id, string status, string processedBy)
        {
            var update = Builders<GdprRequestLog>.Update
                .Set(x => x.Status, status)
                .Set(x => x.ProcessedBy, processedBy)
                .Set(x => x.ProcessedDate, DateTime.UtcNow);

            await _collection.UpdateOneAsync(x => x.Id == id, update);
        }
    }
}