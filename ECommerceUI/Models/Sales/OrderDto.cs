namespace ECommerceUI.Models.Sales
{
    public class OrderDto
    {
        public string Id { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingStatus { get; set; }
        public DateTime OrderDate { get; set; }

        public string? PaymentMethod { get; set; }

        public string? ShippingFullName { get; set; }
        public string? ShippingPhone { get; set; }
    }
}
