using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        // 🔥 ADMIN: CREATE SHIPMENT (per vendor)
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            string orderId,
            string shippingMethod,
            string vendorId)
        {
            await _service.CreateShipmentAsync(orderId, shippingMethod, vendorId);
            return Ok("Shipment created");
        }

        // 🔐 VENDOR: MARK AS SHIPPED
        [HttpPut("{id}/ship")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> Ship(string id)
        {
            var vendorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _service.MarkAsShippedAsync(id, vendorId);

            return Ok("Shipment marked as shipped");
        }

        // 🔐 VENDOR: DELIVERED
        [HttpPut("{id}/deliver")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> Deliver(string id)
        {
            var vendorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _service.MarkAsDeliveredAsync(id, vendorId);

            return Ok("Shipment delivered");
        }

        // 🔐 VENDOR: SHIP WITH TRACKING
        [HttpPut("{id}/ship-with-tracking")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> ShipWithTracking(
            string id,
            [FromBody] string trackingNumber)
        {
            var vendorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _service.MarkAsShippedWithTrackingAsync(
                id,
                trackingNumber,
                vendorId
            );

            return Ok(new { message = "Shipment shipped with tracking" });
        }

        // 🔐 VENDOR: GET THEIR SHIPMENTS ONLY
        [HttpGet("my-shipments")]
        [Authorize(Roles = "Vendor")]
        public async Task<IActionResult> GetMyShipments()
        {
            var vendorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _service.GetByVendorIdAsync(vendorId));
        }

        // 🔥 ADMIN: GET ALL
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // 🔓 PUBLIC / USER
        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();

            return Ok(data);
        }

        // 🔓 USER: TRACK ORDER
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(string orderId)
        {
            return Ok(await _service.GetByOrderIdAsync(orderId));
        }

        // 🔥 ADMIN SEARCH
        [HttpPost("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search([FromBody] ShipmentSearchDto dto)
        {
            return Ok(await _service.SearchAsync(dto));
        }
    }
}