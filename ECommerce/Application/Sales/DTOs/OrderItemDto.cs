namespace ECommerce.Application.Sales.DTOs
{
    public class OrderItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string VendorId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
