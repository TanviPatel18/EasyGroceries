using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Catalog.DTOs;
using ECommerce.Application.Catalog.Interfaces;
using ECommerce.Models.Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace ECommerce.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerService _service;

        public ManufacturersController(IManufacturerService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateManufacturerDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok(dto);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(string id, Manufacturer manufacturer)
        {
            await _service.UpdateAsync(id, manufacturer);
            return Ok(manufacturer);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return Ok("Deleted Successfully");
        }

        // SEARCH
        [HttpGet("search/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            return Ok(await _service.SearchAsync(name));
        }

    }
}
