using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using ECommerceUI.Models.Cart;

namespace ECommerceUI.Services.other
{
    public class CartService
    {
        private readonly HttpClient _http;

        public CartService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CartItemDto>> GetCartAsync()
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get, "api/cart/mycart");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);

                var response = await _http.SendAsync(request);
                if (!response.IsSuccessStatusCode) return new();

                return await response.Content
                    .ReadFromJsonAsync<List<CartItemDto>>() ?? new();
            }
            catch { return new(); }
        }

        public async Task<bool> AddAsync(
            string productId, int quantity = 1)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Post, "api/cart/add");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);
                request.Content = JsonContent.Create(new
                {
                    ProductId = productId,
                    Quantity = quantity
                });

                var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateQuantityAsync(
            string productId, int quantity)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Put, "api/cart/update-quantity");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);
                request.Content = JsonContent.Create(new
                {
                    ProductId = productId,
                    Quantity = quantity
                });

                var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> RemoveAsync(string productId)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Delete,
                    $"api/cart/remove/{productId}");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);

                var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        public async Task<bool> ClearAsync()
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Post, "api/cart/clear");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);

                var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}