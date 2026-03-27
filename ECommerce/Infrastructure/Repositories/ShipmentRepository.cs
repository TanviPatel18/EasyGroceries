using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;

public class ShipmentRepository : IShipmentRepository
{
    private readonly IMongoCollection<Shipment> _collection;

    public ShipmentRepository(MongoDbContext context)
    {
        _collection = context.Shipments;
    }

    public async Task CreateAsync(Shipment shipment)
        => await _collection.InsertOneAsync(shipment);

    public async Task<Shipment?> GetByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return null;

        return await _collection
            .Find(x => x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Shipment>> GetByOrderIdAsync(string orderId)
        => await _collection
            .Find(x => x.OrderId == orderId && !x.IsDeleted)
            .ToListAsync();

    public async Task UpdateAsync(Shipment shipment)
    {
        await _collection.ReplaceOneAsync(
            x => x.Id == shipment.Id && !x.IsDeleted,
            shipment);
    }

    public async Task<List<Shipment>> GetAllAsync()
    {
        return await _collection.Find(x => !x.IsDeleted)
                                .SortByDescending(x => x.CreatedOn)
                                .ToListAsync();
    }

    public async Task<List<Shipment>> SearchAsync(ShipmentSearchDto dto)
    {
        var builder = Builders<Shipment>.Filter;
        var filter = builder.Eq(x => x.IsDeleted, false);

        if (!string.IsNullOrWhiteSpace(dto.OrderId))
            filter &= builder.Eq(x => x.OrderId, dto.OrderId);

        if (!string.IsNullOrWhiteSpace(dto.TrackingNumber))
            filter &= builder.Regex(
                x => x.TrackingNumber,
                new MongoDB.Bson.BsonRegularExpression(dto.TrackingNumber, "i"));

        if (!string.IsNullOrWhiteSpace(dto.Status))
            filter &= builder.Eq(x => x.Status, dto.Status);

        if (!string.IsNullOrWhiteSpace(dto.ShippingMethod))
            filter &= builder.Eq(x => x.ShippingMethod, dto.ShippingMethod);

        if (dto.StartDate.HasValue)
            filter &= builder.Gte(x => x.CreatedOn, dto.StartDate.Value);

        if (dto.EndDate.HasValue)
            filter &= builder.Lte(x => x.CreatedOn, dto.EndDate.Value);

        return await _collection.Find(filter)
                                .SortByDescending(x => x.CreatedOn)
                                .ToListAsync();
    }

    public async Task<Shipment?> GetSingleByOrderIdAsync(string orderId)
    {
        return await _collection
            .Find(x => x.OrderId == orderId && !x.IsDeleted)
            .FirstOrDefaultAsync();
    }
}
