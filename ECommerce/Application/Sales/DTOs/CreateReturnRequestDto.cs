namespace ECommerce.Application.Sales.DTOs
{
    public class CreateReturnRequestDto
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }

        public int Quantity { get; set; }
        public string Reason { get; set; }
    }
}
