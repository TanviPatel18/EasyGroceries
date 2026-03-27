namespace ECommerce.Application.Sales.DTOs
{
    public class OrderDto
    {

        public string Id { get; set; }
        public string CustomerId { get; set; }

        public decimal OrderTotal { get; set; }

        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingStatus { get; set; }

        public DateTime OrderDate { get; set; }

        // ✅ ADD SHIPMENT DATA
        public string? TrackingNumber { get; set; }
        public string? ShippingMethod { get; set; }
    }
}
