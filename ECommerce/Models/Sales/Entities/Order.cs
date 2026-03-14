using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Models.Sales.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }

        public decimal OrderTotal { get; set; }

        public string Currency { get; set; }

        public string OrderStatus { get; set; } = "Placed";
        public string PaymentStatus { get; set; } = "Pending";
        public string ShippingStatus { get; set; } = "Pending";

        public string PaymentMethod { get; set; }
        public string ShippingMethod { get; set; }

        public OrderAddress BillingAddress { get; set; }
        public OrderAddress ShippingAddress { get; set; }


        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? PaidDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? ShipmentId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
