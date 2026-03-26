using ECommerceUI.Models.Catalog.Enums;
using System.Text.Json.Serialization;

namespace ECommerceUI.Models.Catalog
{
    public class ProductVm
    {
        public string? Id { get; set; }
        public string? ProductId { get; set; }  // add this
        public List<string> ImageUrls { get; set; } = new();

        public string? ProductName { get; set; }
        public string? SKU { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType ProductType { get; set; }
        public string CategoryId { get; set; }

        //public ProductType ProductType { get; set; } // Enum
        // For display
        public bool IsSelected { get; set; }
        public double Rating { get; set; }
    }

}
