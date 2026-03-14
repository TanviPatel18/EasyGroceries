using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IReturnRequestRepository
    {
        Task CreateAsync(ReturnRequest request);
        Task<List<ReturnRequest>> GetAllAsync();
        Task<ReturnRequest> GetByIdAsync(string id);
        Task UpdateAsync(ReturnRequest request);
        Task<List<ReturnRequest>> SearchAsync(ReturnRequestSearchDto dto);
    }
}
