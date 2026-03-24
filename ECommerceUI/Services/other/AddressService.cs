using System.Net.Http.Json;
using System.Text.Json;
using ECommerceUI.Models.Account;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace ECommerceUI.Services.other
{
    public class AddressService
    {
        private readonly HttpClient _http;

        public AddressService(HttpClient http)
        {
            _http = http;
        }

        private static JsonSerializerOptions JsonOptions => new()
        {
            PropertyNameCaseInsensitive = true
        };

        private HttpRequestMessage Req(HttpMethod method, string url)
        {
            var r = new HttpRequestMessage(method, url);
            r.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return r;
        }

        // ── Get all addresses ──
        public async Task<List<CustomerAddressDto>> GetMyAddressesAsync()
        {
            try
            {
                var req = Req(HttpMethod.Get, "api/address/my");
                var res = await _http.SendAsync(req);
                if (!res.IsSuccessStatusCode) return new();
                return await res.Content
                    .ReadFromJsonAsync<List<CustomerAddressDto>>(JsonOptions)
                    ?? new();
            }
            catch { return new(); }
        }

        // ── Add address ──
        public async Task<(bool Success, string? Error)> AddAddressAsync(
            CreateAddressDto dto)
        {
            try
            {
                var req = Req(HttpMethod.Post, "api/address/add");
                req.Content = JsonContent.Create(dto);
                var res = await _http.SendAsync(req);
                return res.IsSuccessStatusCode
                    ? (true, null)
                    : (false, "Failed to add address. Please try again.");
            }
            catch { return (false, "Something went wrong."); }
        }

        // ── Update address ──
        public async Task<(bool Success, string? Error)> UpdateAddressAsync(
            UpdateAddressDto dto)
        {
            try
            {
                var req = Req(HttpMethod.Put, "api/address/update");
                req.Content = JsonContent.Create(dto);
                var res = await _http.SendAsync(req);
                return res.IsSuccessStatusCode
                    ? (true, null)
                    : (false, "Failed to update address. Please try again.");
            }
            catch { return (false, "Something went wrong."); }
        }

        // ── Delete address ──
        public async Task<(bool Success, string? Error)> DeleteAddressAsync(
            string id)
        {
            try
            {
                var req = Req(HttpMethod.Delete, $"api/address/{id}");
                var res = await _http.SendAsync(req);
                return res.IsSuccessStatusCode
                    ? (true, null)
                    : (false, "Failed to delete address.");
            }
            catch { return (false, "Something went wrong."); }
        }
    }
}