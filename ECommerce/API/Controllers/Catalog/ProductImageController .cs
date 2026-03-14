using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/admin/productimage")]
    public class ProductImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ProductImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // UPLOAD MULTIPLE IMAGES
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm(Name = "file")] List<IFormFile> files)
        {
            if (files == null || !files.Any())
                return BadRequest("No files uploaded");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "images/products");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var resultUrls = new List<string>();

            foreach (var file in files)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                resultUrls.Add($"images/products/{fileName}");
            }

            return Ok(resultUrls.Select(url => new { imageUrl = url }));
        }

        // REMOVE IMAGE
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