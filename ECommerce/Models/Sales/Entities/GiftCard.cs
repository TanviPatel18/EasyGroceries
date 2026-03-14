using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Models.Sales.Entities
{
    public class GiftCard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CouponCode { get; set; }

        public decimal InitialValue { get; set; }

        public decimal RemainingAmount { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
