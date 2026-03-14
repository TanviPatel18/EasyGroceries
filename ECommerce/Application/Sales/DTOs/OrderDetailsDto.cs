using ECommerce.Models.Sales.Entities;

namespace ECommerce.Application.Sales.DTOs
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

        public OrderAddress BillingAddress { get; set; }
        public OrderAddress ShippingAddress { get; set; }

        public List<OrderItemDto> Products { get; set; }
    }
}
