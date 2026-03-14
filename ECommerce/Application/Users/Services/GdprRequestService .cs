
using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using MongoDB.Driver;

namespace ECommerce.Application.Users.Services
{
    public class GdprRequestService : IGdprRequestService
    {
        private readonly IGdprRequestRepository _repository;

        public GdprRequestService(IGdprRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(CreateGdprRequestDto dto)
        {
            var entity = new GdprRequestLog
            {
                CustomerId = dto.CustomerId,
                CustomerEmail = dto.CustomerEmail,
                RequestType = dto.RequestType,
                RequestDetails = dto.RequestDetails,
                RequestDate = DateTime.UtcNow,
                Status = "Pending",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await _repository.CreateAsync(entity);
        }

        public async Task<List<GdprRequestLog>> SearchAsync(string email, string requestType)
        {
            return await _repository.SearchAsync(email, requestType);
        }

        public async Task UpdateStatusAsync(string id, string status, string processedBy)
        {
            await _repository.UpdateStatusAsync(id, status, processedBy);
        }

        // Get all GDPR requests
        public async Task<List<GdprRequestLog>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Get GDPR request by ID
        public async Task<GdprRequestLog> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }

}
