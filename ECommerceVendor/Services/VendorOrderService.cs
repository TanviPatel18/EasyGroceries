// ECommerceUI/Services/VendorOrderService.cs
using ECommerceVendor.Models;
using ECommerceVendor.Models;
using System.Net.Http.Json;

namespace ECommerceVendor.Services
{
    public class VendorOrderService
    {
        private readonly HttpClient _http;

        public VendorOrderService(HttpClient http) => _http = http;

        public async Task<List<VendorOrderDto>> GetMyOrdersAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<VendorOrderDto>>(
                    "api/vendor/orders") ?? new();
            }
            catch { return new(); }
        }

        public async Task<bool> UpdateStatusAsync(string orderId, string status)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(
                    $"api/vendor/orders/{orderId}/status", status);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}