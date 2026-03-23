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
        private readonly IProductRepository _productRepo; // ← add this

        public CartService(
            ICartRepository cartRepo,
            ICartItemRepository itemRepo,
            IProductRepository productRepo) // ← add this
        {
            _cartRepo = cartRepo;
            _itemRepo = itemRepo;
            _productRepo = productRepo; // ← add this
        }

        public async Task AddToCartAsync(
            string customerId,
            string productId,
            int quantity)
        {
            var cart = await _cartRepo
                .GetByCustomerIdAsync(customerId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    CustomerId = customerId
                };
                await _cartRepo.CreateCartAsync(cart);
            }

            // ✅ Check if item already exists — update quantity
            var existing = (await _itemRepo
                .GetItemsByCartIdAsync(cart.Id))
                .FirstOrDefault(x => x.ProductId == productId);

            if (existing != null)
            {
                await _itemRepo.UpdateQuantityAsync(
                    cart.Id, productId,
                    existing.Quantity + quantity);
            }
            else
            {
                await _itemRepo.AddItemAsync(new ShoppingCartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                });
            }
        }

        // ✅ Now returns full product details
        public async Task<List<CartItemDetailDto>> GetMyCartAsync(
            string customerId)
        {
            var cart = await _cartRepo
                .GetByCustomerIdAsync(customerId);

            if (cart == null)
                return new List<CartItemDetailDto>();

            var items = await _itemRepo
                .GetItemsByCartIdAsync(cart.Id);

            var result = new List<CartItemDetailDto>();

            foreach (var item in items)
            {
                var product = await _productRepo
                    .GetByIdAsync(item.ProductId);

                if (product == null) continue;

                result.Add(new CartItemDetailDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    OldPrice = product.OldPrice,
                    StockQuantity = product.StockQuantity,
                    ImageUrls = product.ImageUrls ?? new()
                });
            }

            return result;
        }

        public async Task RemoveFromCartAsync(
            string customerId, string productId)
        {
            var cart = await _cartRepo
                .GetByCustomerIdAsync(customerId);
            if (cart == null) return;

            await _itemRepo.RemoveItemAsync(cart.Id, productId);
        }

        public async Task ClearCartAsync(string customerId)
        {
            var cart = await _cartRepo
                .GetByCustomerIdAsync(customerId);
            if (cart == null) return;

            await _itemRepo.ClearCartAsync(cart.Id);
        }

        // ✅ Update quantity
        public async Task UpdateQuantityAsync(
            string customerId,
            string productId,
            int quantity)
        {
            var cart = await _cartRepo
                .GetByCustomerIdAsync(customerId);
            if (cart == null) return;

            if (quantity <= 0)
                await _itemRepo.RemoveItemAsync(
                    cart.Id, productId);
            else
                await _itemRepo.UpdateQuantityAsync(
                    cart.Id, productId, quantity);
        }
    }
}