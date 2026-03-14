using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Users
{
    [Route("api/admin/customer-roles")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminCustomerRoleController : ControllerBase
    {
        private readonly ICustomerRoleService _service;

        public AdminCustomerRoleController(ICustomerRoleService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCustomerRoleDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok("Role created successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id, UpdateCustomerRoleDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok("Role updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return Ok("Role deleted successfully");
        }
    }
}
