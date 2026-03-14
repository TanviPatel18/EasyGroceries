namespace ECommerceUI.Models.Sales
{
    public class AdminOrderSearchDto
    {
        public string? OrderId { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ShippingStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
