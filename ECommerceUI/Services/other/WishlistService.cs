using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using ECommerceUI.Models.wishlist;

namespace ECommerceUI.Services.other
{
    public class WishlistService
    {
        private readonly HttpClient _http;

        public WishlistService(HttpClient http)
        {
            _http = http;
        }

        // CHECK IF PRODUCT EXISTS IN WISHLIST
        public async Task<bool> IsInWishlistAsync(string productId)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"api/wishlist/check/{productId}");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);

                var response = await _http.SendAsync(request);
                if (!response.IsSuccessStatusCode) return false;

                var res = await response.Content
                    .ReadFromJsonAsync<CheckResponse>();
                return res?.Exists ?? false;
            }
            catch { return false; }
        }

        // ADD PRODUCT
        public async Task AddAsync(string productId)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    "api/wishlist/add");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);
                request.Content = JsonContent.Create(
                    new { ProductId = productId });

                await _http.SendAsync(request);
            }
            catch { }
        }

        // REMOVE PRODUCT
        public async Task<bool> RemoveAsync(string productId)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Delete,
                    $"api/wishlist/{productId}");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);

                var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // GET COUNT
        public async Task<int> GetCountAsync()
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "api/wishlist/count");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);

                var response = await _http.SendAsync(request);
                if (!response.IsSuccessStatusCode) return 0;

                var res = await response.Content
                    .ReadFromJsonAsync<CountResponse>();
                return res?.count ?? 0;
            }
            catch { return 0; }
        }

        // GET FULL WISHLIST
        public async Task<List<WishlistItem>> GetWishlistAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/wishlist");
                if (!response.IsSuccessStatusCode) return new();

                return await response.Content
                    .ReadFromJsonAsync<List<WishlistItem>>() ?? new();
            }
            catch { return new(); }
        }

        private class CheckResponse
        {
            public bool Exists { get; set; }
        }

        private class CountResponse
        {
            public int count { get; set; }
        }
    }
}