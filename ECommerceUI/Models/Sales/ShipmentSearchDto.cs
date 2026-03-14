namespace ECommerceUI.Models.Sales
{
    public class ShipmentSearchDto
    {
        public string? OrderId { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Status { get; set; }
        public string? ShippingMethod { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
