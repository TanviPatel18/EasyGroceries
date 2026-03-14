using ECommerce.Application.Users.DTOs;
using ECommerce.Models.Users.Entities;

namespace ECommerce.Application.Users.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(string id);
        Task<Customer> CreateAsync(CreateCustomerDto dto);
        Task<List<Customer>> SearchAsync(SearchCustomerDto dto);
        Task UpdateByEmailAsync(string email, UpdateCustomerDto dto);
        Task SoftDeleteByEmailAsync(string email);
    }
}
