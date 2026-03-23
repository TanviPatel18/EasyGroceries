namespace ECommerceUI.Models.Cart
{
    public class CartItemDto
    {

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int Quantity { get; set; }
        public int StockQuantity { get; set; }
        public List<string> ImageUrls { get; set; } = new();
    }
}
