using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Catalog.Entities;
using System.Threading.Tasks;
using ECommerce.Application.Catalog.DTOs;
namespace ECommerce.Application.Catalog.Interfaces
{
    public interface IManufacturerService
    {
        Task<List<Manufacturer>> GetAllAsync();
        Task CreateAsync(CreateManufacturerDto dto);
        Task UpdateAsync(string id, Manufacturer manufacturer);
        Task DeleteAsync(string id);
        Task<List<Manufacturer>> SearchAsync(string name);
    }
}
