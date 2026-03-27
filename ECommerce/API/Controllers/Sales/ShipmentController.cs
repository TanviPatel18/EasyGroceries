using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Sales
{
    [ApiController]
    [Route("api/shipments")]
    [Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _service;

        public ShipmentController(IShipmentService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string orderId, string shippingMethod)
        {
            await _service.CreateShipmentAsync(orderId, shippingMethod);
            return Ok("Shipment created");
        }

        [HttpPut("{id}/ship")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Ship(string id)
        {
            await _service.MarkAsShippedAsync(id);
            return Ok();
        }

        [HttpPut("{id}/deliver")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deliver(string id)
        {
            await _service.MarkAsDeliveredAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(string orderId)
        {
            return Ok(await _service.GetByOrderIdAsync(orderId));
        }

        [HttpPost("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search([FromBody] ShipmentSearchDto dto)
        {
            return Ok(await _service.SearchAsync(dto));
        }
    }
}