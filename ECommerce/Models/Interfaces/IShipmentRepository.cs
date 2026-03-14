using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IShipmentRepository
    {
        Task CreateAsync(Shipment shipment);
        Task<Shipment> GetByIdAsync(string id);
        Task<List<Shipment>> GetByOrderIdAsync(string orderId);
        Task UpdateAsync(Shipment shipment);

        Task<List<Shipment>> GetAllAsync();

        Task<List<Shipment>> SearchAsync(ShipmentSearchDto dto);
    }
}

