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

        public ShipmentService(
            IShipmentRepository shipmentRepo,
            IOrderRepository orderRepo)
        {
            _shipmentRepo = shipmentRepo;
            _orderRepo = orderRepo;
        }

        // 🔥 CREATE SHIPMENT PER VENDOR
        public async Task CreateShipmentAsync(string orderId, string shippingMethod, string vendorId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            var shipment = new Shipment
            {
                OrderId = orderId,
                VendorId = vendorId, // ✅ IMPORTANT
                ShippingMethod = shippingMethod,
                TrackingNumber = GenerateTrackingNumber(),
                Status = "Pending",
                CreatedOn = DateTime.UtcNow
            };

            await _shipmentRepo.CreateAsync(shipment);

            // Optional: keep order status generic
            order.ShippingStatus = "Pending";
            await _orderRepo.UpdateAsync(order);
        }

        // 🔐 VENDOR SAFE: MARK AS SHIPPED
        public async Task MarkAsShippedAsync(string shipmentId, string vendorId)
        {
            var shipment = await _shipmentRepo.GetByIdAsync(shipmentId);
            if (shipment == null)
                throw new Exception("Shipment not found");

            if (shipment.VendorId != vendorId)
                throw new Exception("Unauthorized access");

            shipment.Status = "Shipped";
            shipment.ShippedDate = DateTime.UtcNow;

            await _shipmentRepo.UpdateAsync(shipment);

            await UpdateOrderShippingStatus(shipment.OrderId);
        }

        // 🔐 VENDOR SAFE: DELIVERED
        public async Task MarkAsDeliveredAsync(string shipmentId, string vendorId)
        {
            var shipment = await _shipmentRepo.GetByIdAsync(shipmentId);
            if (shipment == null)
                throw new Exception("Shipment not found");

            if (shipment.VendorId != vendorId)
                throw new Exception("Unauthorized access");

            shipment.Status = "Delivered";
            shipment.DeliveredDate = DateTime.UtcNow;

            await _shipmentRepo.UpdateAsync(shipment);

            await UpdateOrderShippingStatus(shipment.OrderId);
        }

        // 🔐 WITH TRACKING
        public async Task MarkAsShippedWithTrackingAsync(
            string shipmentId, string trackingNumber, string vendorId)
        {
            var shipment = await _shipmentRepo.GetByIdAsync(shipmentId);
            if (shipment == null)
                throw new Exception("Shipment not found");

            if (shipment.VendorId != vendorId)
                throw new Exception("Unauthorized access");

            shipment.Status = "Shipped";
            shipment.ShippedDate = DateTime.UtcNow;
            shipment.TrackingNumber = trackingNumber;

            await _shipmentRepo.UpdateAsync(shipment);

            await UpdateOrderShippingStatus(shipment.OrderId);
        }

        // ✅ GET BY ORDER (MULTIPLE SHIPMENTS NOW)
        public async Task<List<Shipment>> GetByOrderIdAsync(string orderId)
        {
            return await _shipmentRepo.GetByOrderIdAsync(orderId);
        }

        // ✅ GET BY VENDOR (VERY IMPORTANT)
        public async Task<List<Shipment>> GetByVendorIdAsync(string vendorId)
        {
            return await _shipmentRepo.GetByVendorIdAsync(vendorId);
        }

        public async Task<List<Shipment>> GetAllAsync()
            => await _shipmentRepo.GetAllAsync();

        public async Task<Shipment?> GetByIdAsync(string id)
            => await _shipmentRepo.GetByIdAsync(id);

        public async Task<List<Shipment>> SearchAsync(ShipmentSearchDto dto)
            => await _shipmentRepo.SearchAsync(dto);

        // 🔥 SMART ORDER STATUS UPDATE
        private async Task UpdateOrderShippingStatus(string orderId)
        {
            var shipments = await _shipmentRepo.GetByOrderIdAsync(orderId);
            var order = await _orderRepo.GetByIdAsync(orderId);

            if (order == null) return;

            if (shipments.All(s => s.Status == "Delivered"))
            {
                order.ShippingStatus = "Delivered";
                order.OrderStatus = "Completed";
            }
            else if (shipments.Any(s => s.Status == "Shipped"))
            {
                order.ShippingStatus = "PartiallyShipped";
            }

            await _orderRepo.UpdateAsync(order);
        }

        private string GenerateTrackingNumber()
            => "TRK" + DateTime.UtcNow.Ticks.ToString().Substring(10);
    }
}