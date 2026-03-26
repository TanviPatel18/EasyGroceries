namespace ECommerceUI.Models.Catalog
{
    public class ProductReviewDto
    {
        public string Id { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public int Rating { get; set; }
        public string Title { get; set; } = "";
        public string Comment { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
