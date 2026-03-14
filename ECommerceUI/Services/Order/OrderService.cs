using ECommerceUI.Models;
using ECommerceUI.Models.Sales;
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
    }
}