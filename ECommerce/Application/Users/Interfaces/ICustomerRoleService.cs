using ECommerce.Application.Users.DTOs;
using ECommerce.Models.Users.Entities;

namespace ECommerce.Application.Users.Interfaces
{
    public interface ICustomerRoleService
    {
        Task<List<CustomerRole>> GetAllAsync();
        Task<CustomerRole> GetByIdAsync(string id);
        Task CreateAsync(CreateCustomerRoleDto dto);
        Task UpdateAsync(string id, UpdateCustomerRoleDto dto);
        Task DeleteAsync(string id);

    }
}
