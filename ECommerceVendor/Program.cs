using ECommerceVendor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Connect to your API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/") // your API URL
});

await builder.Build().RunAsync();