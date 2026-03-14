using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users;
using ECommerce.Models.Users.Entities;

namespace ECommerce.Application.Users.Services
{
    public class CustomerRoleService : ICustomerRoleService
    {
        private readonly ICustomerRoleRepository _repository;

        public CustomerRoleService(ICustomerRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerRole>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();

            return roles.Select(x => new CustomerRole
            {
                Id = x.Id,
                RoleName = x.RoleName,
                FreeShipping = x.FreeShipping,
                TaxExempt = x.TaxExempt,
                IsActive = x.IsActive,
                IsSystemRole = x.IsSystemRole
            }).ToList();
        }

        public async Task<CustomerRole> GetByIdAsync(string id)
            => await _repository.GetByIdAsync(id);

        public async Task CreateAsync(CreateCustomerRoleDto dto)
        {
            var role = new CustomerRole
            {
                RoleName = dto.RoleName,
                FreeShipping = dto.FreeShipping,
                TaxExempt = dto.TaxExempt,
                IsSystemRole = dto.IsSystemRole
            };

            await _repository.CreateAsync(role);
        }

        public async Task UpdateAsync(string id, UpdateCustomerRoleDto dto)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                throw new Exception("Role not found");

            role.RoleName = dto.RoleName;
            role.FreeShipping = dto.FreeShipping;
            role.TaxExempt = dto.TaxExempt;
            role.IsActive = dto.IsActive;

            await _repository.UpdateAsync(role);
        }

        public async Task DeleteAsync(string id)
            => await _repository.DeleteAsync(id);
    }
}
