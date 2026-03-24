using ECommerce.Application.Users.DTOs;
using ECommerce.Models.Users.Entities;

namespace ECommerce.Application.Users.Interfaces
{
    public interface IAddressService
    {
        Task<List<CustomerAddress>> GetMyAddressesAsync(string customerId);

        Task AddAddressAsync(string customerId, CreateAddressDto dto);

        Task DeleteAddressAsync(string addressId);
    }
}
