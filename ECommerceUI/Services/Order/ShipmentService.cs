using ECommerceUI.Models.Sales;
using System.Net.Http.Json;

namespace ECommerceUI.Services
{
    public class ShipmentService
    {
        private readonly HttpClient _http;

        public ShipmentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ShipmentDto>> GetAll()
        {
            return await _http.GetFromJsonAsync<List<ShipmentDto>>(
                "api/shipments");
        }

        public async Task<List<ShipmentDto>> Search(ShipmentSearchDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "api/shipments/search", dto);

            return await response.Content
                                 .ReadFromJsonAsync<List<ShipmentDto>>();
        }

        public async Task MarkAsShipped(string id)
        {
            await _http.PutAsync($"api/shipments/{id}/ship", null);
        }

        public async Task MarkAsDelivered(string id)
        {
            await _http.PutAsync($"api/shipments/{id}/deliver", null);
        }

        public async Task<ShipmentDto> GetById(string id)
        {
            return await _http.GetFromJsonAsync<ShipmentDto>(
                $"api/shipments/{id}");
        }
    }
}