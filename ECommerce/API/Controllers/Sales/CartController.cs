using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers.Sales
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        private string GetCustomerId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CartItemDto dto)
        {
            await _service.AddToCartAsync(
                GetCustomerId(),
                dto.ProductId,
                dto.Quantity
            );

            return Ok("Product added to cart");
        }

        [HttpGet("mycart")]
        public async Task<IActionResult> Get()
        {
            var cart = await _service.GetMyCartAsync(GetCustomerId());
            return Ok(cart);
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> Remove(string productId)
        {
            await _service.RemoveFromCartAsync(GetCustomerId(), productId);
            return Ok("Product removed from cart");
        }

        [HttpPost("clear")]
        public async Task<IActionResult> Clear()
        {
            await _service.ClearCartAsync(GetCustomerId());
            return Ok("Cart cleared");
        }


        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(
            [FromBody] CartItemDto dto)
        {
            await _service.UpdateQuantityAsync(
                GetCustomerId(),
                dto.ProductId,
                dto.Quantity);
            return Ok("Quantity updated");
        }

        
    }
}
