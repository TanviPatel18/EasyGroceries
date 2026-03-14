using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Application.Sales.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly ICartRepository _cartRepo;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IProductRepository _productRepo;

        public OrderService(
            IOrderRepository orderRepo,
            IOrderItemRepository orderItemRepo,
            ICartRepository cartRepo,
            ICartItemRepository cartItemRepo,
            IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
        }

        // ✅ PLACE ORDER FROM CART WITH SHIPPING ADDRESS
        public async Task PlaceOrderFromCartAsync(string customerId, OrderAddressDto addressDto)
        {
            var cart = await _cartRepo.GetByCustomerIdAsync(customerId);
            if (cart == null)
                throw new Exception("Cart not found");

            var cartItems = await _cartItemRepo.GetItemsByCartIdAsync(cart.Id);
            if (!cartItems.Any())
                throw new Exception("Cart is empty");

            decimal total = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in cartItems)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception("Product not found");

                if (product.StockQuantity < item.Quantity)
                    throw new Exception($"Insufficient stock for {product.ProductName}");

                // Calculate total
                total += item.Quantity * product.Price;

                // Reduce stock
                product.StockQuantity -= item.Quantity;
                await _productRepo.UpdateAsync(product);

                // Prepare order items
                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = product.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            // ✅ Create address object (used for both Shipping & Billing)
            var address = new OrderAddress
            {
                FullName = addressDto.FullName,
                PhoneNumber = addressDto.PhoneNumber,
                Address = addressDto.Address,
                City = addressDto.City,
                ZipCode = addressDto.ZipCode,
                Country = "India"
            };

            // ✅ Create Order
            var order = new Order
            {
                CustomerId = customerId,
                OrderTotal = total,
                Currency = "INR",
                OrderStatus = "Placed",
                PaymentStatus = "Pending",
                ShippingStatus = "Pending",
                OrderDate = DateTime.UtcNow,

                ShippingAddress = address,
                BillingAddress = address   // 🔥 IMPORTANT FIX
            };

            // Save Order
            await _orderRepo.CreateAsync(order);

            // Assign OrderId to order items
            foreach (var item in orderItems)
            {
                item.OrderId = order.Id;
            }

            // Save Order Items
            await _orderItemRepo.AddItemsAsync(orderItems);

            // Clear Cart
            await _cartItemRepo.ClearCartAsync(cart.Id);
        }
        // ✅ GET MY ORDERS
        public async Task<List<Order>> GetMyOrdersAsync(string customerId)
        {
            return await _orderRepo.GetByCustomerIdAsync(customerId);
        }

        // ✅ GET ORDER BY ID
        public async Task<Order?> GetOrderByIdAsync(string customerId, string orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);

            if (order == null || order.CustomerId != customerId)
                return null;

            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepo.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAdminAsync(string orderId)
        {
            return await _orderRepo.GetByIdAsync(orderId);
        }

        public async Task<List<Order>> SearchOrdersAsync(AdminOrderSearchDto searchDto)
        {
            return await _orderRepo.SearchAsync(searchDto);
        }

        public async Task UpdateOrderStatusAsync(string orderId, string orderStatus, string paymentStatus, string shippingStatus)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            order.OrderStatus = orderStatus;
            order.PaymentStatus = paymentStatus;
            order.ShippingStatus = shippingStatus;

            await _orderRepo.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(string orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            order.IsDeleted = true;
            await _orderRepo.UpdateAsync(order);
        }
        public async Task<List<OrderItem>> GetOrderItemsAsync(string orderId)
        {
            return await _orderItemRepo.GetByOrderIdAsync(orderId);
        }
    }
}
