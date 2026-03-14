using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Application.Users.DTOs;


namespace ECommerce.API.Controllers.Users
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/vendors")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await GetAll(); // return all if query is empty

            var allVendors = await _vendorService.GetAllVendorsAsync();
            var filtered = allVendors
                .Where(v => v.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                         || v.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filtered);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _vendorService.GetAllVendorsAsync());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
            => Ok(await _vendorService.GetVendorByIdAsync(id));

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateVendorDto dto)
        {
            await _vendorService.CreateVendorAsync(dto);
            return Ok("Vendor created successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, CreateVendorDto dto)
        {
            await _vendorService.UpdateVendorAsync(id, dto);
            return Ok("Vendor updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _vendorService.DeleteVendorAsync(id);
            return Ok("Vendor deleted successfully");
        }
    }
}
