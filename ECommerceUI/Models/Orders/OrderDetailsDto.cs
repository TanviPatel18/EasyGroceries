namespace ECommerceUI.Models.Orders
{
    public class OrderDetailsDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }

        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public string? PaymentMethod { get; set; }

        public OrderAddressDto? BillingAddress { get; set; }
        public OrderAddressDto? ShippingAddress { get; set; }

        public List<OrderItemDto> Products { get; set; } = new();
    }
}
