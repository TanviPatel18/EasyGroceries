using ECommerce.Models.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryImageRepository : ICategoryImageRepository
    {
        private readonly IWebHostEnvironment _env;

        public CategoryImageRepository(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var folder = Path.Combine(_env.WebRootPath, "images/categories");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"images/categories/{fileName}";
        }

        public Task<bool> DeleteAsync(string imageUrl)
        {
            var fullPath = Path.Combine(_env.WebRootPath, imageUrl);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}