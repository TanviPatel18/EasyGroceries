using ECommerceUI.Models;
using ECommerceUI.Models.Orders;
using ECommerceUI.Models.Sales;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Json;


namespace ECommerceUI.Services
{
    public class OrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<OrderDto>> GetAll()
        {
            return await _http.GetFromJsonAsync<List<OrderDto>>(
                "api/admin/orders/getall");
        }

        public async Task<OrderDetailsDto> GetById(string id)
        {
            return await _http.GetFromJsonAsync<OrderDetailsDto>(
                $"api/admin/orders/{id}");
        }

        public async Task UpdateStatus(string id, string orderStatus, string paymentStatus, string shippingStatus)
        {
            await _http.PutAsync(
                $"api/admin/orders/update-status?id={id}&orderStatus={orderStatus}&paymentStatus={paymentStatus}&shippingStatus={shippingStatus}",
                null);
        }

        public async Task Delete(string id)
        {
            await _http.DeleteAsync($"api/admin/orders/delete/{id}");
        }

        public async Task<List<OrderDto>> Search(AdminOrderSearchDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "api/admin/orders/search", dto);

            return await response.Content.ReadFromJsonAsync<List<OrderDto>>();
        }
        public async Task<(bool success, string orderId, string error)> PlaceOrderAsync(PlaceOrderRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/orders/place", request);

                if (!response.IsSuccessStatusCode)
                {
                    return (false, string.Empty, "Order failed");
                }

                var result = await response.Content.ReadFromJsonAsync<OrderResponseDto>();

                return (true, result?.OrderId ?? string.Empty, null);
            }
            catch (Exception ex)
            {
                return (false, string.Empty, ex.Message);
            }
        }

        public async Task AddToCartAsync(string productId, int quantity)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/cart/add");
                request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
                request.Content = JsonContent.Create(new { productId, quantity });
                await _http.SendAsync(request);
            }
            catch { }
        }
        public async Task<(bool success, string? error)> CancelOrderAsync(string orderId)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/orders/{orderId}/cancel");

                if (!response.IsSuccessStatusCode)
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    return (false, msg);
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<List<OrderDto>> GetMyOrdersAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<OrderDto>>("api/orders/myorders")
                       ?? new List<OrderDto>();
            }
            catch
            {
                return new List<OrderDto>();
            }
        }
        public async Task<List<OrderItemDto>> GetOrderItemsAsync(string orderId)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<OrderItemDto>>(
                    $"api/orders/{orderId}/items")
                    ?? new List<OrderItemDto>();
            }
            catch
            {
                return new List<OrderItemDto>();
            }
        }

    }
}