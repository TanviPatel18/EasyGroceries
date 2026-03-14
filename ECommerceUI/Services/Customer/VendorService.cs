using System.Net.Http.Json;
using ECommerceUI.Models.Users; // Make sure your DTOs are in this namespace

public class VendorService
{
    private readonly HttpClient _http;

    public VendorService(HttpClient http)
    {
        _http = http;
    }

    // Create a new vendor
    public async Task Create(CreateVendorDto dto)
    {
        await _http.PostAsJsonAsync("api/admin/vendors/Create", dto);
    }

    // Get all vendors
    public async Task<List<VendorDto>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<VendorDto>>("api/admin/vendors/GetAll");
    }

    // Get vendor by Id
    public async Task<VendorDto> GetById(string id)
    {
        return await _http.GetFromJsonAsync<VendorDto>($"api/admin/vendors/GetById/{id}");
    }

    // Update vendor by Id
    public async Task Update(string id, CreateVendorDto dto)
    {
        await _http.PutAsJsonAsync($"api/admin/vendors/Update/{id}", dto);
    }

    // Delete vendor by Id
    public async Task Delete(string id)
    {
        await _http.DeleteAsync($"api/admin/vendors/Delete/{id}");
    }
}