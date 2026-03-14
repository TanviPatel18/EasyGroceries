using ECommerceUI.Models.Catalog.Enums;

namespace ECommerceUI.Models.Catalog
{
    public class ProductSearchDto
    {
        //public string Name { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string? ManufacturerId { get; set; }
        public ProductType? ProductType { get; set; }

        public bool? Published { get; set; }
    }
}
