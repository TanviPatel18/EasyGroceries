using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Users
{
    [ApiController]
    [Route("api/customers/gdpr")]
    public class GdprRequestsController : ControllerBase
    {
        private readonly IGdprRequestService _service;

        public GdprRequestsController(IGdprRequestService service)
        {
            _service = service;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: api/customers/gdpr/{id}
        public async Task<IActionResult> GetById(string id)
        {
            var request = await _service.GetByIdAsync(id);
            if (request == null)
                return NotFound();
            return Ok(request);
        }

        // Search (for grid)
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string email, [FromQuery] string requestType)
        {
            var result = await _service.SearchAsync(email, requestType);
            return Ok(result);
        }

        // Create request
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGdprRequestDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok("GDPR request created successfully");
        }

        // Process request
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(string id, string status)
        {
            await _service.UpdateStatusAsync(id, status, "Admin");
            return Ok("Updated successfully");
        }
    }
}
