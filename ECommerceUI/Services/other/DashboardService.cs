using ECommerceUI.Models.Admin;
using System.Net.Http.Json;

namespace ECommerceUI.Services.other
{
    public class DashboardService
    {
        private readonly HttpClient _http;

        public DashboardService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            var result = await _http.GetFromJsonAsync<DashboardDto>(
                "api/admin/dashboard");

            return result ?? new DashboardDto();
        }
    }
}