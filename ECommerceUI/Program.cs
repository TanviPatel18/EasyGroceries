using ECommerceUI;
using ECommerceUI.Services;
using ECommerceUI.Services.Customer;
using ECommerceUI.Services.Order;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Text.Json;
using ECommerceUI.Services.other;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ✅ Register handler first
builder.Services.AddScoped<AuthMessageHandler>();

// ✅ Default HttpClient WITH auth handler
builder.Services.AddScoped(sp =>
{
    var js = sp.GetRequiredService<Microsoft.JSInterop.IJSRuntime>();
    var handler = new AuthMessageHandler(js)
    {
        InnerHandler = new HttpClientHandler()
    };

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:5001/")
    };
});

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ManufacturerService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<GdprRequestService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<ShipmentService>();
builder.Services.AddScoped<ReturnRequestService>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
        sp.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddScoped<WishlistService>();
// ✅ Add this line
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AddressService>();
//builder.Services.AddScoped<RecentProductService>();




await builder.Build().RunAsync();