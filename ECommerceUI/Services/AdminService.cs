using ECommerceUI.Models;
using ECommerceUI.Models.Catalog;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Reflection;
using ECommerceUI.Models.Users;

namespace ECommerceUI.Services
{
    public class AdminService
    {
        private readonly HttpClient _http;

        public AdminService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            return await _http.GetFromJsonAsync<List<CategoryDto>>(
                "api/catalog/categories/GetAll");
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            return await _http.GetFromJsonAsync<List<CustomerDto>>(
                "api/users/customers/GetAll");
        }

        public async Task<List<VendorDto>> GetVendors()
        {
            return await _http.GetFromJsonAsync<List<VendorDto>>(
                "api/admin/vendors/GetAll");
        }

        public async Task<List<GdprRequestDto>> GetGdprRequests()
        {
            return await _http.GetFromJsonAsync<List<GdprRequestDto>>(
                "api/customers/gdpr");
        }
    }
}
