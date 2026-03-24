using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;

namespace ECommerce.Application.Users.Services
{
    public class AddressService:IAddressService
    {
        private readonly ICustomerAddressRepository _repo;

        public AddressService(ICustomerAddressRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerAddress>> GetMyAddressesAsync(string customerId)
        {
            return await _repo.GetByCustomerIdAsync(customerId);
        }

        public async Task AddAddressAsync(string customerId, CreateAddressDto dto)
        {
            var address = new CustomerAddress
            {
                CustomerId = customerId,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                Country = "India",
                IsDefault = dto.IsDefault
            };

            await _repo.CreateAsync(address);
        }

        public async Task DeleteAddressAsync(string addressId)
        {
            await _repo.DeleteAsync(addressId);
        }
    }
}
