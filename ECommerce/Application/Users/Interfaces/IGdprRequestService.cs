using ECommerce.Application.Users.DTOs;
using ECommerce.Models.Users.Entities;

namespace ECommerce.Application.Users.Interfaces
{
    public interface IGdprRequestService
    {
        Task CreateAsync(CreateGdprRequestDto dto);
        Task<GdprRequestLog> GetByIdAsync(string id);

        // NEW: Get all GDPR requests
        Task<List<GdprRequestLog>> GetAllAsync();
        Task<List<GdprRequestLog>> SearchAsync(string email, string requestType);
        Task UpdateStatusAsync(string id, string status, string processedBy);
    }
}
