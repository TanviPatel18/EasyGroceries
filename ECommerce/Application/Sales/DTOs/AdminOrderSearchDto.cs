namespace ECommerce.Application.Sales.DTOs
{
    public class AdminOrderSearchDto
    {
        public string? OrderId { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ShippingStatus { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? PaymentMethod { get; set; }
        public string? BillingPhone { get; set; }
        public string? BillingEmail { get; set; }
        public string? BillingLastName { get; set; }
        public string? BillingCountry { get; set; }
    }
}
