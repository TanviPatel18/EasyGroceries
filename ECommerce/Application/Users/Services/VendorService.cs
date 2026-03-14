using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ECommerce.Application.Users.Services
{
    public class VendorService:IVendorService
    {
        private readonly IVendorRepository _vendorRepo;
        private readonly ICustomerRoleRepository _roleRepo;

        public VendorService(IVendorRepository vendorRepo, ICustomerRoleRepository roleRepo)
        {
            _vendorRepo = vendorRepo;
            _roleRepo = roleRepo;
        }

        public async Task<List<VendorResponseDto>> GetAllVendorsAsync()
        {
            var vendors = await _vendorRepo.GetAllAsync();
            return vendors.Select(v => new VendorResponseDto
            {
                Id = v.Id,
                Name = v.Name,
                Email = v.Email,
                ContactPhone = v.ContactPhone,
                CompanyName = v.CompanyName,
                IsActive = v.IsActive,
                CustomerRoleName = v.CustomerRoleName
            }).ToList();
        }

        public async Task<VendorResponseDto> GetVendorByIdAsync(string id)
        {
            var v = await _vendorRepo.GetByIdAsync(id);
            if (v == null) return null;

            return new VendorResponseDto
            {
                Id = v.Id,
                Name = v.Name,
                Email = v.Email,
                ContactPhone = v.ContactPhone,
                CompanyName = v.CompanyName,
                IsActive = v.IsActive,
                CustomerRoleName = v.CustomerRoleName
            };
        }

        public async Task CreateVendorAsync(CreateVendorDto dto)
        {
            // 1️⃣ Get Role from CustomerRole collection
            var role = await _roleRepo.GetByNameAsync("Vendor");
            if (role == null) throw new System.Exception("Vendor role not found");

            var vendor = new Vendor
            {
                Name = dto.Name,
                Email = dto.Email,
                ContactPhone = dto.ContactPhone,
                CompanyName = dto.CompanyName,
                IsActive = true,
                CreatedOn = System.DateTime.UtcNow,
                CustomerRoleId = role.Id,
                CustomerRoleName = role.RoleName
            };

            await _vendorRepo.CreateAsync(vendor);
        }

        public async Task UpdateVendorAsync(string id, CreateVendorDto dto)
        {
            var vendor = await _vendorRepo.GetByIdAsync(id);
            if (vendor == null) throw new System.Exception("Vendor not found");

            vendor.Name = dto.Name;
            vendor.Email = dto.Email;
            vendor.ContactPhone = dto.ContactPhone;
            vendor.CompanyName = dto.CompanyName;

            await _vendorRepo.UpdateAsync(vendor);
        }

        public async Task DeleteVendorAsync(string id)
        {
            await _vendorRepo.DeleteAsync(id);
        }
    }
}
