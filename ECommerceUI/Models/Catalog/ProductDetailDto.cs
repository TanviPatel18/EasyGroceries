namespace ECommerceUI.Models.Catalog
{
    public class ProductDetailDto
    {
        public string Id { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string Description { get; set; } = "";
        public string Brand { get; set; } = "";
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public string? Weight { get; set; }
        public string? Dimensions { get; set; }
        public string? Sku { get; set; }
        public string CategoryId { get; set; }
    }
}
