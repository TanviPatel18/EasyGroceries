using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Sales.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepo;
        private readonly IOrderRepository _orderRepo;

        public ShipmentService(IShipmentRepository shipmentRepo, IOrderRepository orderRepo)
        {
            _shipmentRepo = shipmentRepo;
            _orderRepo = orderRepo;
        }

        public async Task CreateShipmentAsync(string orderId, string shippingMethod)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            var shipment = new Shipment
            {
                OrderId = orderId,
                ShippingMethod = shippingMethod,
                TrackingNumber = GenerateTrackingNumber(),
                Status = "Pending"
            };

            await _shipmentRepo.CreateAsync(shipment);

            order.ShippingStatus = "Pending";
            await _orderRepo.UpdateAsync(order);
        }

        public async Task MarkAsShippedAsync(string shipmentId)
        {
            var shipment = await _shipmentRepo.GetByIdAsync(shipmentId);
            if (shipment == null)
                throw new Exception("Shipment not found");

            shipment.Status = "Shipped";
            shipment.ShippedDate = DateTime.UtcNow;

            await _shipmentRepo.UpdateAsync(shipment);

            var order = await _orderRepo.GetByIdAsync(shipment.OrderId);
            if (order != null)
            {
                order.ShippingStatus = "Shipped";
                await _orderRepo.UpdateAsync(order);
            }
        }

        public async Task MarkAsDeliveredAsync(string shipmentId)
        {
            var shipment = await _shipmentRepo.GetByIdAsync(shipmentId);
            if (shipment == null)
                throw new Exception("Shipment not found");

            shipment.Status = "Delivered";
            shipment.DeliveredDate = DateTime.UtcNow;

            await _shipmentRepo.UpdateAsync(shipment);

            var order = await _orderRepo.GetByIdAsync(shipment.OrderId);
            if (order != null)
            {
                order.ShippingStatus = "Delivered";
                await _orderRepo.UpdateAsync(order);
            }
        }
        public async Task<Shipment?> GetSingleByOrderIdAsync(string orderId)
        {
            var shipments = await _shipmentRepo.GetByOrderIdAsync(orderId);
            return shipments.FirstOrDefault();
        }
        public async Task<List<Shipment>> GetAllAsync()
            => await _shipmentRepo.GetAllAsync();

        public async Task<Shipment?> GetByIdAsync(string id)
            => await _shipmentRepo.GetByIdAsync(id);

        public async Task<List<Shipment>> GetByOrderIdAsync(string orderId)
            => await _shipmentRepo.GetByOrderIdAsync(orderId);

        public async Task<List<Shipment>> SearchAsync(ShipmentSearchDto dto)
            => await _shipmentRepo.SearchAsync(dto);

        private string GenerateTrackingNumber()
            => "TRK" + DateTime.UtcNow.Ticks.ToString().Substring(10);
    }
}