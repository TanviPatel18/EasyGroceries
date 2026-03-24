using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ECommerce.Application.Catalog.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(string id);
        Task CreateAsync(CreateProductDto dto);
        Task UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(string id);
        Task<List<Product>> SearchAsync(ProductSearchDto search);
    }
}