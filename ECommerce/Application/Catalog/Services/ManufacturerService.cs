using ECommerce.Application.Catalog.DTOs;
using ECommerce.Application.Catalog.Interfaces;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Interfaces;

namespace ECommerce.Application.Catalog.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _repository;

        public ManufacturerService(IManufacturerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(CreateManufacturerDto dto)
        {
            await _repository.CreateAsync(dto);
        }
        public async Task UpdateAsync(string id, Manufacturer manufacturer)
        { 
            await _repository.UpdateAsync(id, manufacturer);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<List<Manufacturer>> SearchAsync(string name)
        { 
            return await _repository.SearchAsync(name);
        }
    }
}
