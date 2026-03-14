using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Sales.Services
{
    public class ReturnRequestService : IReturnRequestService
    {
        private readonly IReturnRequestRepository _repo;

        public ReturnRequestService(IReturnRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(CreateReturnRequestDto dto)
        {
            var request = new ReturnRequest
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                CustomerId = dto.CustomerId,
                Quantity = dto.Quantity,
                Reason = dto.Reason,
                Status = "Pending"
            };

            await _repo.CreateAsync(request);
        }

        public async Task<List<ReturnRequest>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<List<ReturnRequest>> SearchAsync(ReturnRequestSearchDto dto)
            => await _repo.SearchAsync(dto);

        public async Task ApproveAsync(string id)
        {
            var request = await _repo.GetByIdAsync(id);
            request.Status = "Approved";
            await _repo.UpdateAsync(request);
        }

        public async Task RejectAsync(string id)
        {
            var request = await _repo.GetByIdAsync(id);
            request.Status = "Rejected";
            await _repo.UpdateAsync(request);
        }
    }
}
