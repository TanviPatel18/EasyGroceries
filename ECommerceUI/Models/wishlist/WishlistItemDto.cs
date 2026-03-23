namespace ECommerceUI.Models.wishlist
{
    public class WishlistItemDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public DateTime AddedOn { get; set; }
    }
}
