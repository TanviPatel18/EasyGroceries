using ECommerceUI.Models.Catalog;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class ProductService
{
    private readonly HttpClient _http;
    public ProductService(HttpClient http) => _http = http;

    public async Task<List<string>> UploadImages(List<IBrowserFile> files)
    {
        var content = new MultipartFormDataContent();

        foreach (var file in files)
        {
            var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10 MB max
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(streamContent, "file", file.Name);
        }

        var response = await _http.PostAsync("api/admin/productimage/upload", content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<List<ImageResponse>>();
        return result.Select(r => r.ImageUrl).ToList();
    }  


    public async Task<List<ProductVm>> GetAllProducts() =>
        await _http.GetFromJsonAsync<List<ProductVm>>("api/catalog/products/GetAll");

    public async Task<List<ProductVm>> Search(ProductSearchDto dto) =>
        await _http.PostAsJsonAsync("api/catalog/products/search", dto)
                   .ContinueWith(async t => await t.Result.Content.ReadFromJsonAsync<List<ProductVm>>())
                   .Unwrap();

    public async Task Create(CreateProductDto dto) =>
        await _http.PostAsJsonAsync("api/catalog/products/Create", dto);

    public async Task Update(UpdateProductDto dto) =>
        await _http.PutAsJsonAsync("api/catalog/products/Update", dto);

    public async Task Delete(string id) =>
        await _http.DeleteAsync($"api/catalog/products/{id}");

    public async Task<ProductDetailDto?> GetById(string id)
    {
        return await _http.GetFromJsonAsync<ProductDetailDto>(
            $"api/catalog/products/{id}");
    }


    public async Task<List<ProductReviewDto>> GetReviews(string productId)
    {
        return await _http.GetFromJsonAsync<List<ProductReviewDto>>(
            $"api/products/{productId}/reviews");
    }

    public async Task<List<CategoryVm>> GetCategories() =>
        await _http.GetFromJsonAsync<List<CategoryVm>>("api/catalog/categories/GetAll");
}
public class ImageResponse
{
    public string ImageUrl { get; set; }
}