using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ECommerce.Models.Users.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerRoleId { get; set; }
        public string CustomerRole { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastActivityDate { get; set; }

        public string IpAddress { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsTaxExempt { get; set; } = false;
        public string Newsletter { get; set; }
        public string Vendor { get; set; }
        public string? AdminComment { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
