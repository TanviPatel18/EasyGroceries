using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
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
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                Landmark = dto.Landmark,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                Country = dto.Country,
                AddressType = dto.AddressType,
                IsDefault = dto.IsDefault
            };
            await _repo.CreateAsync(address);
        }

        // ✅ New method
        public async Task UpdateAddressAsync(string customerId, UpdateAddressDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null || existing.CustomerId != customerId)
                throw new UnauthorizedAccessException("Address not found.");

            existing.FullName = dto.FullName;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.AddressLine1 = dto.AddressLine1;
            existing.AddressLine2 = dto.AddressLine2;
            existing.Landmark = dto.Landmark;
            existing.City = dto.City;
            existing.State = dto.State;
            existing.ZipCode = dto.ZipCode;
            existing.Country = dto.Country;
            existing.AddressType = dto.AddressType;
            existing.IsDefault = dto.IsDefault;

            await _repo.UpdateAsync(existing);
        }

        public async Task DeleteAddressAsync(string addressId)
        {
            await _repo.DeleteAsync(addressId);
        }
    }
}
