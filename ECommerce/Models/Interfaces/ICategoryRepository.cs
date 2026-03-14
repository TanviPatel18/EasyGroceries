using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task CreateAsync(Category category);
        Task<List<Category>> SearchAsync(CategorySearchDto dto);

        Task<Category> GetByNameAsync(string name);
        Task<Category> GetByIdAsync(string id);
        Task UpdateAsync(Category category);

    }
}
