using System.Net.Http.Json;
using ECommerceUI.Models.Catalog;

public class CategoryService
{
    private readonly HttpClient _http;

    public CategoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task Create(CreateCategoryDto dto)
    {
        await _http.PostAsJsonAsync(
            "api/catalog/categories/Create", dto);
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<CategoryDto>>(
            "api/catalog/categories/GetAll");
    }

    public async Task<List<CategoryDto>> Search(CategorySearchDto dto)
    {
        var response = await _http.PostAsJsonAsync(
            "api/catalog/categories/search", dto);

        return await response.Content
            .ReadFromJsonAsync<List<CategoryDto>>();
    }

    public async Task<CategoryDto> GetById(string id)
    {
        return await _http.GetFromJsonAsync<CategoryDto>(
            $"api/catalog/categories/{id}");
    }

    public async Task Update(UpdateCategoryDto dto)
    {
        await _http.PutAsJsonAsync(
            "api/catalog/categories/Update",
            dto);
    }
    public async Task<List<ProductVm>> GetProductsByCategory(string categoryId)
    {
        var dto = new CategoryFilterDto
        {
            Id = categoryId
        };

        var response = await _http.PostAsJsonAsync(
            "api/catalog/categories/filter-products", dto);

        response.EnsureSuccessStatusCode(); // throws if 400

        return await response.Content.ReadFromJsonAsync<List<ProductVm>>();
    }
}
