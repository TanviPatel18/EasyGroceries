using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers.Users
{
    [ApiController]
    [Route("api/address")]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        private string GetCustomerId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet("my")]
        public async Task<IActionResult> MyAddresses()
        {
            var result = await _service.GetMyAddressesAsync(GetCustomerId());
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAddress(CreateAddressDto dto)
        {
            await _service.AddAddressAsync(GetCustomerId(), dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAddressAsync(id);
            return Ok();
        }
    }
}