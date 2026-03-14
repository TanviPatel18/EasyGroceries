using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Sales.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly ICartItemRepository _itemRepo;

        public CartService(
            ICartRepository cartRepo,
            ICartItemRepository itemRepo)
        {
            _cartRepo = cartRepo;
            _itemRepo = itemRepo;
        }

        public async Task AddToCartAsync(string customerId, string productId, int quantity)
        {
            var cart = await _cartRepo.GetByCustomerIdAsync(customerId);

            if (cart == null)
            {
                cart = new ShoppingCart { CustomerId = customerId };
                await _cartRepo.CreateCartAsync(cart);
            }

            await _itemRepo.AddItemAsync(new ShoppingCartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity
            });
        }

        public async Task<List<CartItemDto>> GetMyCartAsync(string customerId)
        {
            var cart = await _cartRepo.GetByCustomerIdAsync(customerId);

            if (cart == null)
                return new List<CartItemDto>();

            var items = await _itemRepo.GetItemsByCartIdAsync(cart.Id);

            return items.Select(x => new CartItemDto
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList();
        }

        public async Task RemoveFromCartAsync(string customerId, string productId)
        {
            var cart = await _cartRepo.GetByCustomerIdAsync(customerId);
            if (cart == null) return;

            await _itemRepo.RemoveItemAsync(cart.Id, productId);
        }

        public async Task ClearCartAsync(string customerId)
        {
            var cart = await _cartRepo.GetByCustomerIdAsync(customerId);
            if (cart == null) return;

            await _itemRepo.ClearCartAsync(cart.Id);
        }
    }
}
