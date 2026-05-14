using ECommerce.Application.Sales.DTOs;
using ECommerce.Application.Sales.Interfaces;
using ECommerce.Application.Users.DTOs;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Sales.Entities;
using ECommerce.Models.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Application.Sales.Services
{
    public class OrderService : IOrderService
    {
        private readonly IVendorRepository _vendorRepository;

        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly ICartRepository _cartRepo;
        private readonly ICartItemRepository _cartItemRepo;
        private readonly IProductRepository _productRepo;
        private readonly IShipmentService _shipmentService;
        public OrderService(
            IOrderRepository orderRepo,
            IOrderItemRepository orderItemRepo,
            ICartRepository cartRepo,
            ICartItemRepository cartItemRepo,
            IProductRepository productRepo,
            IShipmentService shipmentService,
            IVendorRepository vendorRepository)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _cartRepo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
            _shipmentService = shipmentService;
            _vendorRepository = vendorRepository;
        }

        // ✅ PLACE ORDER FROM CART WITH SHIPPING ADDRESS
        public async Task<string> PlaceOrderFromCartAsync(string customerId, OrderAddressDto addressDto)
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

                total += item.Quantity * product.Price;

                // Update stock
                product.StockQuantity -= item.Quantity;
                await _productRepo.UpdateAsync(product);

                // ✅ Fetch Vendor
                var vendor = await _vendorRepository.GetByIdAsync(product.VendorId);

                // ✅ Create OrderItem
                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = product.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    VendorId = product.VendorId,
                    VendorName = vendor?.Name ?? ""
                });
            }

            // ✅ Address
            var address = new OrderAddress
            {
                FullName = addressDto.FullName,
                PhoneNumber = addressDto.PhoneNumber,
                Address = addressDto.Address,
                City = addressDto.City,
                State = addressDto.State,
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
                PaymentStatus = addressDto.PaymentMethod == "COD" ? "Pending" : "Paid",
                ShippingStatus = "Pending",
                PaymentMethod = addressDto.PaymentMethod,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = address,
                BillingAddress = address
            };

            await _orderRepo.CreateAsync(order);

            // ✅ Attach OrderId to items
            foreach (var item in orderItems)
                item.OrderId = order.Id;

            await _orderItemRepo.AddItemsAsync(orderItems);

            // 🔥 VERY IMPORTANT: Create shipment per vendor (Amazon logic)
            var groupedByVendor = orderItems.GroupBy(x => x.VendorId);

            foreach (var group in groupedByVendor)
            {
                await _shipmentService.CreateShipmentAsync(
                    order.Id,
                    "Standard",
                    group.Key // VendorId
                );
            }

            // ✅ Clear cart
            await _cartItemRepo.ClearCartAsync(cart.Id);

            return order.Id;
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




        // ── Vendor: Get Orders ───────────────────────────────────────
        public async Task<List<VendorOrderDto>> GetOrdersByVendorAsync(
            string vendorId)
        {
            var vendorItems =
                await _orderItemRepo.GetByVendorIdAsync(vendorId);

            var grouped = vendorItems.GroupBy(i => i.OrderId);
            var result = new List<VendorOrderDto>();

            foreach (var group in grouped)
            {
                var order = await _orderRepo.GetByIdAsync(group.Key);
                if (order == null) continue;

                result.Add(new VendorOrderDto
                {
                    OrderId = order.Id,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    ShippingStatus = order.ShippingStatus,
                    CustomerName = order.ShippingAddress?.FullName ?? "",
                    CustomerPhone = order.ShippingAddress?.PhoneNumber ?? "",
                    ShippingCity = order.ShippingAddress?.City ?? "",
                    ShippingState = order.ShippingAddress?.State ?? "",
                    VendorTotal = group.Sum(i => i.Quantity * i.UnitPrice),
                    Items = group.Select(i => new VendorOrderItemDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                });
            }

            return result.OrderByDescending(x => x.OrderDate).ToList();
        }

        // ── Vendor: Update Status (Processing / ReadyToShip) ─────────
        public async Task UpdateVendorOrderStatusAsync(
            string orderId, string status)
        {
            var allowed = new[] { "Processing", "ReadyToShip" };
            if (!allowed.Contains(status))
                throw new Exception("Vendor can only set Processing or ReadyToShip");

            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null) throw new Exception("Order not found");

            order.OrderStatus = status;
            await _orderRepo.UpdateAsync(order);
        }
    }
}
