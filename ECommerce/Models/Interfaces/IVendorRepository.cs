using ECommerce.Models.Users.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetAllAsync();
        Task<Vendor> GetByIdAsync(string id);
        Task<Vendor> GetByEmailAsync(string email);
        Task CreateAsync(Vendor vendor);
        Task UpdateAsync(Vendor vendor);
        Task DeleteAsync(string id);

        Task<Vendor> GetByNameAsync(string name);

    }
}
