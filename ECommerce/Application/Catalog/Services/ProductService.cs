using ECommerce.Application.Catalog.DTOs;
using ECommerce.Application.Catalog.Interfaces;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Catalog.Enums;
using ECommerce.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Catalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;


        public ProductService(
                IProductRepository repo,
                ICategoryRepository categoryRepository,
                IManufacturerRepository manufacturerRepository,
                IVendorRepository vendorRepository)
        {
            _repo = repo;
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
            _vendorRepository = vendorRepository;
        }


        public async Task CreateAsync(CreateProductDto dto)
        {
            var category = await _categoryRepository.GetByNameAsync(dto.CategoryName);
            var manufacturer = await _manufacturerRepository.GetByNameAsync(dto.ManufacturerName);
            var vendor = await _vendorRepository.GetByNameAsync(dto.VendorName);

            var product = new Product
            {
                ProductName = dto.ProductName,
                SKU = dto.SKU,
                CategoryId = category?.Id,
                ManufacturerId = manufacturer?.Id,
                VendorId = vendor?.Id,
                ProductType = dto.ProductType,
                ImageUrls = dto.ImageUrls,  // ⭐ VERY IMPORTANT

                Published = dto.Published,
                Price = dto.Price,
                OldPrice = dto.OldPrice,
                ProductCost = dto.ProductCost,
                StockQuantity = dto.StockQuantity,
                MinCartQty = dto.MinCartQty,
                MaxCartQty = dto.MaxCartQty,
                SeName = dto.SeName,
                MetaTitle = dto.MetaTitle,
                MetaKeywords = dto.MetaKeywords,
                MetaDescription = dto.MetaDescription,
                CreatedOn = DateTime.UtcNow
            };

            await _repo.CreateAsync(product);
        }


        public async Task UpdateAsync(UpdateProductDto dto)
        {
            // 1️⃣ Get the existing product
            var product = await _repo.GetByIdAsync(dto.Id);
            if (product == null)
                throw new Exception("Product not found");

            // 2️⃣ Update basic fields
            product.ProductName = dto.ProductName;
            product.SKU = dto.SKU;
            product.CategoryId = dto.CategoryId;
            product.ManufacturerId = dto.ManufacturerId;
            product.VendorId = dto.VendorId;

            // 3️⃣ Update ProductType as string
            product.ProductType = dto.ProductType; // ✅ direct enum


            // 4️⃣ Update pricing & stock
            product.Price = dto.Price;
            product.OldPrice = dto.OldPrice;
            product.ProductCost = dto.ProductCost;
            product.StockQuantity = dto.StockQuantity;
            product.MinCartQty = dto.MinCartQty;
            product.MaxCartQty = dto.MaxCartQty;

            // 5️⃣ Published status
            product.Published = dto.Published;

            // 6️⃣ Update Images (replace old images)
            product.ImageUrls.Clear();
            foreach (var url in dto.ImageUrls)
            {
                product.ImageUrls.Add(url);
            }

            // 7️⃣ SEO & meta
            product.SeName = dto.SeName;
            product.MetaTitle = dto.MetaTitle;
            product.MetaKeywords = dto.MetaKeywords;
            product.MetaDescription = dto.MetaDescription;

            // 8️⃣ Updated timestamp
            product.UpdatedOn = DateTime.UtcNow;

            // 9️⃣ Save to repository
            await _repo.UpdateAsync(product);
        }



        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<List<Product>> SearchAsync(ProductSearchDto search)
        {
            return await _repo.SearchAsync(search);
        }
        public async Task<Product?> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }


    }
}