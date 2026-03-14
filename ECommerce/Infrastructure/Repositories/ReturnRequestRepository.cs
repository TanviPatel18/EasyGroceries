using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class ReturnRequestRepository : IReturnRequestRepository
    {
        private readonly IMongoCollection<ReturnRequest> _collection;

        public ReturnRequestRepository(MongoDbContext context)
        {
            _collection = context.ReturnRequests;
        }

        public async Task CreateAsync(ReturnRequest request)
            => await _collection.InsertOneAsync(request);

        public async Task<List<ReturnRequest>> GetAllAsync()
            => await _collection.Find(x => !x.IsDeleted)
                                .SortByDescending(x => x.CreatedOn)
                                .ToListAsync();

        public async Task<ReturnRequest> GetByIdAsync(string id)
            => await _collection.Find(x => x.Id == id && !x.IsDeleted)
                                .FirstOrDefaultAsync();

        public async Task UpdateAsync(ReturnRequest request)
            => await _collection.ReplaceOneAsync(x => x.Id == request.Id, request);

        public async Task<List<ReturnRequest>> SearchAsync(ReturnRequestSearchDto dto)
        {
            var builder = Builders<ReturnRequest>.Filter;
            var filter = builder.Eq(x => x.IsDeleted, false);

            if (!string.IsNullOrWhiteSpace(dto.OrderId))
                filter &= builder.Eq(x => x.OrderId, dto.OrderId);

            if (!string.IsNullOrWhiteSpace(dto.Status))
                filter &= builder.Eq(x => x.Status, dto.Status);

            if (dto.StartDate.HasValue)
                filter &= builder.Gte(x => x.CreatedOn, dto.StartDate.Value);

            if (dto.EndDate.HasValue)
                filter &= builder.Lte(x => x.CreatedOn, dto.EndDate.Value);

            return await _collection.Find(filter)
                                    .SortByDescending(x => x.CreatedOn)
                                    .ToListAsync();
        }
    }
}
