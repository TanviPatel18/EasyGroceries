using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using ECommerce.Persistence;
//using ECommerce.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ECommerce.API.Controllers.Users
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/users/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _service.GetByIdAsync(id);
            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchCustomerDto dto)
        {
            var result = await _service.SearchAsync(dto);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateByEmail(
        [FromQuery] string email,
        UpdateCustomerDto dto)
        {
            await _service.UpdateByEmailAsync(email, dto);
            return Ok("Customer updated successfully");
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            var customer = await _service.CreateAsync(dto);
            return Ok(customer);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteByEmail([FromQuery] string email)
        {
            await _service.SoftDeleteByEmailAsync(email);
            return Ok("Customer deleted successfully");
        }

        // ✅ Add these 2 methods to existing CustomersController.cs
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var customers = await _service.GetAllAsync();
            var customer = customers.FirstOrDefault(x => x.Email == email);
            if (customer == null) return NotFound();

            return Ok(new
            {
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.Phone,
                customer.RegistrationDate
            });
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateMe(
            [FromBody] UpdateCustomerDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            await _service.UpdateByEmailAsync(email, dto);
            return Ok("Profile updated successfully");
        }

    }
}
