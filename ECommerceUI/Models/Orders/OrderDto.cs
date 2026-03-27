using ECommerceUI.Models.Sales;

namespace ECommerceUI.Models.Orders
{
    public class OrderDto
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string ShippingStatus { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public OrderAddressDto? ShippingAddress { get; set; }
        public OrderAddressDto? BillingAddress { get; set; }

        public string? TrackingNumber { get; set; }
        public string? ShippingMethod { get; set; }
    }
}
