using ECommerce.Models.Catalog.Enums;

namespace ECommerce.Application.Catalog.DTOs
{
    public class ProductSearchDto
    {
        public string? ProductName { get; set; }
        public string? CategoryId { get; set; }
        public string? ManufacturerId { get; set; }
        public ProductType? ProductType { get; set; }
        public bool? Published { get; set; }
        public string? SKU { get; set; }
    }
}
