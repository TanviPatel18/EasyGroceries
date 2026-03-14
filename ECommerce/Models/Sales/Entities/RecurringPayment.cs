using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Models.Sales.Entities
{
    public class RecurringPayment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }

        public int CycleLength { get; set; }

        public string CyclePeriod { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime StartDate { get; set; }
    }

}

