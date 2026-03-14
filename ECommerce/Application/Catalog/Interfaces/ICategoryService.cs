using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ECommerce.Application.Catalog.Interfaces
{
    public interface ICategoryService
    {
        Task UpdateAsync(UpdateCategoryDto dto);
        Task<Category> GetById(string id);
        Task CreateAsync(CreateCategoryDto dto);
        Task<List<CategoryDto>> GetAllAsync();
        Task<List<CategoryDto>> SearchAsync(CategorySearchDto dto);

    }
}
