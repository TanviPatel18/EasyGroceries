using ECommerce.Models.Users.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IGdprRequestRepository
    {
        Task CreateAsync(GdprRequestLog entity);
        Task<List<GdprRequestLog>> SearchAsync(string email, string requestType);
        Task UpdateStatusAsync(string id, string status, string processedBy);

        Task<GdprRequestLog> GetByIdAsync(string id);
        Task<List<GdprRequestLog>> GetAllAsync();
    }
}