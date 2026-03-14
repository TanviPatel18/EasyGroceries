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
    [Route("api/catalog/categories")]
    public class CategoriesController:ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            await _categoryService.CreateAsync(dto);
            return Ok(new { message = "Category created successfully" });
        }
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] CategorySearchDto dto)
        {
            var result = await _categoryService.SearchAsync(dto);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDto dto)
        {
            await _categoryService.UpdateAsync(dto);

            return Ok(new { message = "Category updated successfully" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(string id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound();

            var dto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
                Published = category.Published,
                ImageUrl = category.ImageUrl
            };

            return Ok(dto);
        }
    }
}
