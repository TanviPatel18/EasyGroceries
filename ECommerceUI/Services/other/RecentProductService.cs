using ECommerceUI.Models.Catalog;
using Microsoft.JSInterop;
using System.Text.Json;

namespace ECommerceUI.Services.other
{
    public class RecentProductService
    {
        private readonly IJSRuntime _js;

        public RecentProductService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task Add(ProductVm product)
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "recent_products");

            var list = string.IsNullOrEmpty(json)
                ? new List<ProductVm>()
                : JsonSerializer.Deserialize<List<ProductVm>>(json)!;

            // remove duplicate
            list.RemoveAll(p => p.Id == product.Id);

            // add at top
            list.Insert(0, product);

            // keep only 20
            list = list.Take(20).ToList();

            await _js.InvokeVoidAsync("localStorage.setItem",
                "recent_products",
                JsonSerializer.Serialize(list));
        }

        public async Task<List<ProductVm>> Get()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "recent_products");

            if (string.IsNullOrEmpty(json))
                return new List<ProductVm>();

            return JsonSerializer.Deserialize<List<ProductVm>>(json)!;
        }
    }
}
