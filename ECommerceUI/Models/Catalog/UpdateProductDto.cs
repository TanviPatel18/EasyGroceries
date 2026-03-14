using ECommerceUI.Models.Catalog.Enums;

namespace ECommerceUI.Models.Catalog
{
    public class UpdateProductDto
    {
        public string Id { get; set; }

        public string ProductName { get; set; }
        public string SKU { get; set; }

        public string CategoryId { get; set; }
        public string ManufacturerId { get; set; }
        public string VendorId { get; set; }

        public ProductType ProductType { get; set; }


        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }

        public int StockQuantity { get; set; }
        public int MinCartQty { get; set; }
        public int MaxCartQty { get; set; }

        public bool Published { get; set; }

        public List<string> ImageUrls { get; set; } = new();


        public string SeName { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

    }
}

