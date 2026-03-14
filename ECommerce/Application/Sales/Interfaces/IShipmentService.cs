using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Sales.Interfaces
{
    public interface IShipmentService
    {
        Task CreateShipmentAsync(string orderId, string shippingMethod);
        Task MarkAsShippedAsync(string shipmentId);
        Task MarkAsDeliveredAsync(string shipmentId);
        Task<List<Shipment>> GetByOrderIdAsync(string orderId);
        Task<List<Shipment>> GetAllAsync();
        Task<Shipment> GetByIdAsync(string id);
        Task<List<Shipment>> SearchAsync(ShipmentSearchDto dto);
    }
}
