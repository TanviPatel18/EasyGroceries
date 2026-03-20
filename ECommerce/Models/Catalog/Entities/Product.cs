using ECommerce.Models.Catalog.Enums;
using ECommerce.Models.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Models.Catalog.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public string SKU { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ManufacturerId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType ProductType { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string VendorId { get; set; }

        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }

        public int MinCartQty { get; set; }
        public int MaxCartQty { get; set; }

        public string SeName { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

    }
}
