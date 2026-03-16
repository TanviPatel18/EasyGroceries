using ECommerce.Application.Catalog.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Catalog.DTOs;

namespace ECommerce.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/admin/categoryimage")]
    public class CategoryImageController : ControllerBase
    {
        private readonly ICategoryImageService _service;

        public CategoryImageController(ICategoryImageService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] CategoryImageUploadDto dto)
        {
            if (dto.File == null)
                return BadRequest("No file uploaded");

            var imageUrl = await _service.UploadAsync(dto.File);

            return Ok(new { imageUrl });
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return BadRequest("Image url required");

            var result = await _service.DeleteAsync(imageUrl);

            if (!result)
                return NotFound("Image not found");

            return Ok(new { message = "Image removed successfully" });
        }
    }
}