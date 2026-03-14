using ECommerce.Models.Users.Entities;
using ECommerce.Application.Users.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ECommerce.Application.Users.Interfaces
{
    public interface IVendorService
    {
        Task<List<VendorResponseDto>> GetAllVendorsAsync();
        Task<VendorResponseDto> GetVendorByIdAsync(string id);
        Task CreateVendorAsync(CreateVendorDto dto);
        Task UpdateVendorAsync(string id, CreateVendorDto dto);
        Task DeleteVendorAsync(string id);
    }
}
