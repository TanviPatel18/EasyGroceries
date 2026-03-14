using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ECommerce.Models.Users.Entities
{
    public class OnlineCustomer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }

        public string CustomerType { get; set; }  // Guest / Registered

        public string IpAddress { get; set; }

        public string Location { get; set; }

        public DateTime LastActivityDate { get; set; }

        public string LastVisitedPage { get; set; }

        public string SessionId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
