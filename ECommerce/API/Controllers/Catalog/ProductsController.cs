using ECommerce.Application.Catalog.DTOs;
using ECommerce.Application.Catalog.Interfaces;
using ECommerce.Application.Catalog.Services;
using ECommerce.Models.Catalog.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/catalog/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService productService)
        {
            _service = productService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.SearchAsync(new ProductSearchDto());

            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                SKU = p.SKU,
                Price = p.Price,
                OldPrice = p.OldPrice,
                StockQuantity = p.StockQuantity,
                Published = p.Published,
                ProductType = p.ProductType,
                ImageUrls = p.ImageUrls ?? new List<string>() // ✅ FIXED NAME
            }).ToList();

            return Ok(result);
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateAsync(dto);
            return Ok();
        }


        [HttpPost("search")]
        public async Task<IActionResult> Search(ProductSearchDto dto)
        {
            var result = await _service.SearchAsync(dto);
            return Ok(result);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProductDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Product updated");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(new UpdateProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                ManufacturerId = product.ManufacturerId,
                VendorId = product.VendorId,
                Price = product.Price,
                OldPrice = product.OldPrice,
                ProductCost = product.ProductCost,
                StockQuantity = product.StockQuantity,
                MinCartQty = product.MinCartQty,
                MaxCartQty = product.MaxCartQty,
                Published = product.Published,
                SeName = product.SeName,
                MetaTitle = product.MetaTitle,
                MetaKeywords = product.MetaKeywords,
                MetaDescription = product.MetaDescription,
                ImageUrls = product.ImageUrls,
                ProductType = product.ProductType
            });
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return Ok("Product deleted");
        }
    }
}