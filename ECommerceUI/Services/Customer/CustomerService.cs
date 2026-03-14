using System.Net.Http.Json;
using ECommerceUI.Models.Users;

public class CustomerService
{
    private readonly HttpClient _http;

    public CustomerService(HttpClient http)
    {
        _http = http;
    }

    // ================= GET ALL =================
    public async Task<List<CustomerDto>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<CustomerDto>>(
            "api/users/customers/GetAll")
            ?? new List<CustomerDto>();
    }

    // ================= ADVANCED SEARCH =================
    public async Task<List<CustomerDto>> Search(SearchCustomerDto dto)
    {
        var response = await _http.PostAsJsonAsync(
            "api/users/customers/search", dto);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<List<CustomerDto>>()
            ?? new List<CustomerDto>();
    }

    // ================= DELETE =================
    public async Task Delete(string email)
    {
        var response = await _http.DeleteAsync(
            $"api/users/customers/delete?email={email}");

        response.EnsureSuccessStatusCode();
    }

    // ================= GET BY ID =================
    public async Task<CustomerDto> GetById(string id)
    {
        return await _http.GetFromJsonAsync<CustomerDto>(
            $"api/users/customers/GetById/{id}");
    }
}