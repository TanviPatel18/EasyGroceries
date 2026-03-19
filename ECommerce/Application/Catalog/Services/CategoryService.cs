using ECommerce.Application.Catalog.DTOs;
using ECommerce.Application.Catalog.Interfaces;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;



namespace ECommerce.Application.Catalog.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repo;

        private readonly IProductRepository _productRepo;

        public CategoryService(ICategoryRepository repo, IProductRepository productRepo)
        {
            _repo = repo;
            _productRepo = productRepo;
        }
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Published = c.Published,
                DisplayOrder = c.DisplayOrder,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                DisplayOrder = dto.DisplayOrder,
                Published = true,
                ImageUrl = dto.ImageUrl
            };

            await _repo.CreateAsync(category);
        }
        public async Task<List<CategoryDto>> SearchAsync(CategorySearchDto dto)
        {
            var categories = await _repo.SearchAsync(dto);


            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Published = c.Published,
                DisplayOrder = c.DisplayOrder,
                ImageUrl = c.ImageUrl
            }).ToList();
        }
        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var category = await _repo.GetByIdAsync(dto.Id);

            if (category == null)
                throw new Exception("Category not found");

            category.Name = dto.Name;
            category.DisplayOrder = dto.DisplayOrder;
            category.Published = dto.Published;
            category.ImageUrl = dto.ImageUrl;

            await _repo.UpdateAsync(category);
        }
        public async Task<Category> GetById(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<ProductDto>> GetProductsByCategoryAsync(CategoryFilterDto dto)
        {
            // Step 1: Get Category by Id or Name
            var category = await _repo.GetByNameOrIdAsync(dto.Id, dto.Name);

            if (category == null)
                throw new Exception("Category not found");

            // Step 2: Get Products
            var products = await _productRepo.GetByCategoryIdAsync(category.Id);

            // Step 3: Map to DTO
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                SKU = p.SKU,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Published = p.Published,
                ProductType = p.ProductType,
                ImageUrls = p.ImageUrls ?? new List<string>()
            }).ToList();
        }

    }
}
