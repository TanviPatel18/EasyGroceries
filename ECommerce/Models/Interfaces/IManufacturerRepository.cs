using ECommerce.Application.Catalog.DTOs;
using ECommerce.Models.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetAllAsync();
        Task CreateAsync(CreateManufacturerDto dto);
        Task UpdateAsync(string id, Manufacturer manufacturer);
        Task DeleteAsync(string id);
        Task<Manufacturer> GetByNameAsync(string name);

        Task<List<Manufacturer>> SearchAsync(string name);
    }
}
