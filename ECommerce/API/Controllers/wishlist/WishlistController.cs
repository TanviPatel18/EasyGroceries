using ECommerce.Application.Wishlist.DTOs;
using ECommerce.Application.Wishlist.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers.wishlist
{
    [ApiController]
    [Route("api/wishlist")]
    [Authorize]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _service;

        public WishlistController(IWishlistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _service
                .GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(
                    [FromBody] AddToWishlistDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.ProductId))
                return BadRequest("ProductId is required");
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _service.AddAsync(userId, dto.ProductId);
            return Ok(new { message = "Added to wishlist" });
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Remove(
            string productId)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _service.RemoveAsync(userId, productId);
            return Ok(new { message = "Removed from wishlist" });
        }

        [HttpGet("check/{productId}")]
        public async Task<IActionResult> Check(
            string productId)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Ok(new { exists = false });

            var exists = await _service
                .ExistsAsync(userId, productId);
            return Ok(new { Exists = exists });
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var count = await _service.GetCountAsync(userId);

            return Ok(new { count });
        }
    }
}
