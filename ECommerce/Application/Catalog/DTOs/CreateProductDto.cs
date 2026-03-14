using ECommerce.Models.Catalog.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;



namespace ECommerce.Application.Catalog.DTOs
{
    
        public class CreateProductDto
        {
            
        public string ProductName { get; set; }
        public string SKU { get; set; }
        
        public ProductType ProductType { get; set; }
        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }
        public string VendorName { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public bool Published { get; set; }

        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }

        public int StockQuantity { get; set; }
        public int MinCartQty { get; set; }
        public int MaxCartQty { get; set; }

        public string SeName { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
    }

    
}
