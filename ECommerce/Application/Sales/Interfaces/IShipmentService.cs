using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;
using System.Collections;

namespace ECommerce.Application.Sales.Interfaces
{
    public interface IShipmentService
    {
        Task CreateShipmentAsync(string orderId, string shippingMethod, string vendorId);

        //Task<List<Shipment>> GetByVendorIdAsync(string vendorId);
        // 🔐 Vendor-safe methods
        Task MarkAsShippedAsync(string shipmentId, string vendorId);

        Task MarkAsDeliveredAsync(string shipmentId, string vendorId);

        Task MarkAsShippedWithTrackingAsync(
            string shipmentId,
            string trackingNumber,
            string vendorId
        );

        // ✅ Multi-shipment support
        Task<List<Shipment>> GetByOrderIdAsync(string orderId);

        // ✅ Vendor-specific shipments
        Task<List<Shipment>> GetByVendorIdAsync(string vendorId);

        Task<List<Shipment>> GetAllAsync();

        Task<Shipment?> GetByIdAsync(string id);

        Task<List<Shipment>> SearchAsync(ShipmentSearchDto dto);
    }
}
