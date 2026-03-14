using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Models.Users.Entities
{

    public class CustomerRole
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string RoleName { get; set; }

        public bool FreeShipping { get; set; }

        public bool TaxExempt { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemRole { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
