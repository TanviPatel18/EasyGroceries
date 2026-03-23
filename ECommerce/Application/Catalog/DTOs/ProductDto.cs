using ECommerce.Models.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Text;
namespace ECommerce.Application.Catalog.DTOs
{
    public class ProductDto
    {
        public List<string> ImageUrls { get; set; } = new();
        public decimal OldPrice { get; set; }
        public ProductType ProductType { get; set; } // Enum

        public string Id { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

