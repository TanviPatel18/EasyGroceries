namespace ECommerceUI.Models.Sales
{
    public class ShipmentDto
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public string ShippingMethod { get; set; }
        public decimal ShippingCost { get; set; }

        public string Status { get; set; }

        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
