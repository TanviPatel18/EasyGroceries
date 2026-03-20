using Microsoft.AspNetCore.Mvc;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/admin/productimage")]
    public class ProductImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly MongoDbContext _db;

        public ProductImageController(
            IWebHostEnvironment env,
            MongoDbContext db)
        {
            _env = env;
            _db = db;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm(Name = "file")] List<IFormFile> files,
            [FromQuery] string categoryId = "")
        {
            if (files == null || !files.Any())
                return BadRequest("No files uploaded");

            // ── get category folder name from DB ──
            var folderName = "products";

            if (!string.IsNullOrEmpty(categoryId))
            {
                var category = await _db.Categories
                    .Find(c => c.Id == categoryId)
                    .FirstOrDefaultAsync();

                if (category != null)
                {
                    folderName = "products/" +
                        category.Name
                            .ToLower()
                            .Trim()
                            .Replace(" ", "-");
                }
            }

            var uploadsFolder = Path.Combine(
                _env.WebRootPath, "images", folderName);

            // ── auto create folder if not exists ──
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var resultUrls = new List<string>();

            foreach (var file in files)
            {
                var originalName = file.FileName ?? "image";
                var fileName = Path.GetFileName(originalName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(
                    filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                resultUrls.Add($"images/{folderName}/{fileName}");
            }

            return Ok(resultUrls.Select(url => new { imageUrl = url }));
        }

        [HttpDelete("remove")]
        public IActionResult Remove([FromQuery] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return BadRequest("Image url required");

            var fullPath = Path.Combine(_env.WebRootPath, imageUrl);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return Ok(new { message = "Image removed successfully" });
            }

            return NotFound("Image not found");
        }
    }
}