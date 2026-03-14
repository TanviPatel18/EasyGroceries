namespace ECommerceUI.Models.Sales
{
    public class ReturnRequestDto
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string CustomerId { get; set; }

        public int Quantity { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }

        public int QuantityReturnedToStock { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
