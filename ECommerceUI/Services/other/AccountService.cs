using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using ECommerceUI.Models.Account;

namespace ECommerceUI.Services.other
{
    public class AccountService
    {
        private readonly HttpClient _http;

        public AccountService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ProfileDto?> GetProfileAsync()
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "api/users/customers/me");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);

                var response = await _http.SendAsync(request);
                if (!response.IsSuccessStatusCode) return null;

                return await response.Content
                    .ReadFromJsonAsync<ProfileDto>(
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch { return null; }
        }

        public async Task<bool> UpdateProfileAsync(
            UpdateProfileDto dto)
        {
            try
            {
                var request = new HttpRequestMessage(
                    HttpMethod.Put,
                    "api/users/customers/me");
                request.SetBrowserRequestCredentials(
                    BrowserRequestCredentials.Include);
                request.Content = JsonContent.Create(dto);

                var response = await _http.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}