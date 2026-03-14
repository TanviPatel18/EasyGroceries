using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _collection;

        public OrderRepository(MongoDbContext context)
        {
            _collection = context.Orders;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _collection
                .Find(x => !x.IsDeleted)
                .ToListAsync();
        }
        public async Task CreateAsync(Order order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task<List<Order>> GetByCustomerIdAsync(string customerId)
        {
            return await _collection
                .Find(x => x.CustomerId == customerId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(string orderId)
        {
            return await _collection
                .Find(x => x.Id == orderId && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == order.Id,
                order
            );
        }

        public async Task<List<Order>> SearchAsync(AdminOrderSearchDto dto)
        {
            var query = _collection.AsQueryable();

            query = query.Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(dto.OrderId))
                query = query.Where(x => x.Id == dto.OrderId);

            if (!string.IsNullOrEmpty(dto.OrderStatus))
                query = query.Where(x => x.OrderStatus == dto.OrderStatus);

            if (!string.IsNullOrEmpty(dto.PaymentStatus))
                query = query.Where(x => x.PaymentStatus == dto.PaymentStatus);

            if (!string.IsNullOrEmpty(dto.ShippingStatus))
                query = query.Where(x => x.ShippingStatus == dto.ShippingStatus);

            if (dto.StartDate.HasValue)
                query = query.Where(x => x.OrderDate >= dto.StartDate.Value);

            if (dto.EndDate.HasValue)
                query = query.Where(x => x.OrderDate <= dto.EndDate.Value);

            return await Task.FromResult(query.ToList());
        }
    }
}

