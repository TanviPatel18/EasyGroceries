using System.Net.Http.Json;
using ECommerceUI.Models.Sales;

namespace ECommerceUI.Services.Order
{
    public class ReturnRequestService
    {
        private readonly HttpClient _http;

        public ReturnRequestService(HttpClient http)
        {
            _http = http;
        }

        // Get all
        public async Task<List<ReturnRequestDto>> GetAll()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ReturnRequestDto>>("api/returnrequests");
                return result ?? new List<ReturnRequestDto>();
            }
            catch
            {
                return new List<ReturnRequestDto>();
            }
        }

        // Search
        public async Task<List<ReturnRequestDto>> Search(ReturnRequestSearchDto dto)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/returnrequests/search", dto);
                return await response.Content.ReadFromJsonAsync<List<ReturnRequestDto>>() ?? new List<ReturnRequestDto>();
            }
            catch
            {
                return new List<ReturnRequestDto>();
            }
        }

        // Approve
        public async Task Approve(string id)
        {
            await _http.PutAsync($"api/returnrequests/{id}/approve", null);
        }

        // Reject
        public async Task Reject(string id)
        {
            await _http.PutAsync($"api/returnrequests/{id}/reject", null);
        }

        // Create (optional)
        public async Task Create(CreateReturnRequestDto dto)
        {
            await _http.PostAsJsonAsync("api/returnrequests/create", dto);
        }
    }
}