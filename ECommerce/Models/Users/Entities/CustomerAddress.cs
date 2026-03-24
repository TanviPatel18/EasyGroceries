using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Models.Users.Entities
{
    public enum AddressType { Home, Work, Other }  // ✅ defined here to avoid circular dependency

    public class CustomerAddress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        // ✅ Replaced "Address" with these 3:
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; } = "India";

        // ✅ New:
        public AddressType AddressType { get; set; } = AddressType.Home;

        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; } = false;

        // ✅ New:
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}