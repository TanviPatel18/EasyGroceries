using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Models.Sales.Entities
{
    public class ReturnRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }

        public int Quantity { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected, Completed

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}