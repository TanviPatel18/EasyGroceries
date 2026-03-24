using ECommerce.Models.Users.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface ICustomerAddressRepository
    {
        Task<List<CustomerAddress>> GetByCustomerIdAsync(string customerId);

        Task<CustomerAddress?> GetByIdAsync(string id);

        Task CreateAsync(CustomerAddress address);

        Task UpdateAsync(CustomerAddress address);

        Task DeleteAsync(string id);
    }
}
