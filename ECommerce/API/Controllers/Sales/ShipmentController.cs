using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using ECommerce.Models.Sales.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Sales
{
    [ApiController]
    [Route("api/shipments")]
    //[Authorize(Roles = "Admin")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _service;

        public ShipmentController(IShipmentService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string orderId, string shippingMethod)
        {
            await _service.CreateShipmentAsync(orderId, shippingMethod);
            return Ok("Shipment created");
        }

        [HttpPut("{id}/ship")]
        public async Task<IActionResult> Ship(string id)
        {
            await _service.MarkAsShippedAsync(id);
            return Ok("Marked as shipped");
        }

        [HttpPut("{id}/deliver")]
        public async Task<IActionResult> Deliver(string id)
        {
            await _service.MarkAsDeliveredAsync(id);
            return Ok("Marked as delivered");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var shipment = await _service.GetByIdAsync(id);
            return Ok(shipment);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] ShipmentSearchDto dto)
        {
            var result = await _service.SearchAsync(dto);
            return Ok(result);
        }
    }
}
