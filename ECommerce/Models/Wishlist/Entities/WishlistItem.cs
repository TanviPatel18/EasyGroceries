using ECommerce.Models.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Models.Wishlist.Entities
{
    public class WishlistItem : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = string.Empty;

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
