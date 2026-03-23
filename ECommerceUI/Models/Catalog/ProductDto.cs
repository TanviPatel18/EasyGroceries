using ECommerceUI.Models.Catalog.Enums;

namespace ECommerceUI.Models.Catalog
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }
        //public ProductType ProductType { get; set; }    
        public List<string> ImageUrls { get; set; } = new();
        public DateTime CreatedOn { get; set; }

        public decimal OldPrice { get; set; }
    }
}

