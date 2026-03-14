using System.Net.Http;
using System.Net.Http.Json;
using ECommerceUI.Models.Users;

namespace ECommerceUI.Services.Customer
{
    public class GdprRequestService
    {
        private readonly HttpClient _http;

        public GdprRequestService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<GdprRequestDto>> GetAll()
        {
            return await _http.GetFromJsonAsync<List<GdprRequestDto>>("api/customers/gdpr/all")
                   ?? new List<GdprRequestDto>();
        }

        public async Task<List<GdprRequestDto>> Search(string email, string requestType)
        {
            var url = $"api/customers/gdpr?email={email}&requestType={requestType}";
            return await _http.GetFromJsonAsync<List<GdprRequestDto>>(url)
                   ?? new List<GdprRequestDto>();
        }

        
       
        public async Task Create(CreateGdprRequestDto dto)
        {
            await _http.PostAsJsonAsync("api/customers/gdpr", dto);
        }

        public async Task UpdateStatus(string id, string status)
        {
            await _http.PutAsJsonAsync($"api/customers/gdpr/{id}?status={status}", new { });
        }
    }
}