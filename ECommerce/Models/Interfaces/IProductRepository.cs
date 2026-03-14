using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.Catalog.DTOs;


namespace ECommerce.Models.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string id);
        Task<Product?> GetByIdAsync(string id);

        Task<List<Product>> SearchAsync(ProductSearchDto search);
    }
}
