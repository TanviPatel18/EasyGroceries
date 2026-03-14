using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Models.Users.Entities
{
    public class GdprRequestLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CustomerId { get; set; }   // null for deleted users

        [BsonRequired]
        public string CustomerEmail { get; set; }

        [BsonRequired]
        public string RequestType { get; set; }   // Export, Delete

        public string RequestDetails { get; set; }

        [BsonRequired]
        public DateTime RequestDate { get; set; }

        public string ProcessedBy { get; set; }

        public DateTime? ProcessedDate { get; set; }

        [BsonRequired]
        public string Status { get; set; }   // Pending, Completed, Rejected

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }

}
