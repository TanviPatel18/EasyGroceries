using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Shipment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string OrderId { get; set; }

    public string TrackingNumber { get; set; }

    public string? CarrierName { get; set; }
    public string ShippingMethod { get; set; }

    public decimal ShippingCost { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

    public string? DeliveredBy { get; set; }

    //public List<ShipmentStatusHistory> StatusHistory { get; set; }

    public bool IsReturned { get; set; }
    public DateTime? ReturnDate { get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
