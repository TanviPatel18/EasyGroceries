using ECommerce.Application.Sales.DTOs;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Sales.Interfaces
{
    public interface IReturnRequestService
    {
        Task CreateAsync(CreateReturnRequestDto dto);
        Task<List<ReturnRequest>> GetAllAsync();
        Task<List<ReturnRequest>> SearchAsync(ReturnRequestSearchDto dto);
        Task ApproveAsync(string id);
        Task RejectAsync(string id);
    }
}
