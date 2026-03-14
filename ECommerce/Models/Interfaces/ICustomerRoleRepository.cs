using ECommerce.Models.Users.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface ICustomerRoleRepository
    {
        Task<List<CustomerRole>> GetAllAsync();
        Task<CustomerRole> GetByIdAsync(string id);
        Task<CustomerRole> GetByNameAsync(string roleName);
        Task CreateAsync(CustomerRole role);
        Task UpdateAsync(CustomerRole role);
        Task DeleteAsync(string id);
    }
}
