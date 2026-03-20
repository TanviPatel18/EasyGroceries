using ECommerceUI.Models.Catalog.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceUI.Models.Catalog
{
    public class CreateProductDto
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string? SKU { get; set; }

        public string? ShortDescription { get; set; }
        public string? FullDescription { get; set; }

        // IMPORTANT (Name-based logic for backend)
        public string? CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? VendorName { get; set; }

        public bool Published { get; set; }
        public string CategoryId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }
        public int MaxCartQty { get; set; } = 1;

        public int StockQuantity { get; set; }
        public int MinCartQty { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        
        public ProductType ProductType { get; set; }

        public string? SeName { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
    }
}
