using ECommerceUI.Models.Catalog;
using System.Net.Http.Json;

public class ManufacturerService
{
    private readonly HttpClient _http;

    public ManufacturerService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ManufacturerDto>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<ManufacturerDto>>(
            "api/Manufacturers/GetAll");
    }

    public async Task Create(CreateManufacturerDto dto)
    {
        await _http.PostAsJsonAsync(
            "api/Manufacturers/Create", dto);
    }

    public async Task Update(string id, ManufacturerDto dto)
    {
        await _http.PutAsJsonAsync(
            $"api/Manufacturers/Update?id={id}", dto);
    }
    public async Task<List<ManufacturerDto>> Search(string name)
    {
        return await _http.GetFromJsonAsync<List<ManufacturerDto>>(
            $"api/Manufacturers/search/{name}");
    }
    public async Task Delete(string id)
    {
        await _http.DeleteAsync(
            $"api/Manufacturers/Delete/{id}");
    }

    
}