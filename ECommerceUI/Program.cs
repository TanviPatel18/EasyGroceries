using ECommerceUI;
using ECommerceUI.Services;
using ECommerceUI.Services.Customer;
using ECommerceUI.Services.Order;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
    options.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: true));

    return new HttpClient
    {
        BaseAddress = new Uri("https://localhost:5001/")
    };
});


builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ManufacturerService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<GdprRequestService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
        sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<ShipmentService>();
builder.Services.AddScoped<ReturnRequestService>();
//builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
//        sp.GetRequiredService<CustomAuthStateProvider>());


await builder.Build().RunAsync();
