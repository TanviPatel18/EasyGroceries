using ECommerce.Application.Catalog.Interfaces;
using ECommerce.Models.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Catalog.Services
{
    public class CategoryImageService : ICategoryImageService
    {
        private readonly ICategoryImageRepository _repo;

        public CategoryImageService(ICategoryImageRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            return await _repo.UploadAsync(file);
        }

        public async Task<bool> DeleteAsync(string imageUrl)
        {
            return await _repo.DeleteAsync(imageUrl);
        }
    }
}