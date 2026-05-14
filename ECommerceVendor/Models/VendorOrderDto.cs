namespace ECommerceVendor.Models
{
    public class VendorOrderDto
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string ShippingStatus { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingState { get; set; } = string.Empty;
        public decimal VendorTotal { get; set; }
        public List<VendorOrderItemDto> Items { get; set; } = new();
    }
}
