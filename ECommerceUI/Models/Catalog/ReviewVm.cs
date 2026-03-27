namespace ECommerceUI.Models.Catalog
{
    public class ReviewVm
    {
        public string Id { get; set; }
        public string ProductId { get; set; }

        public string CustomerName { get; set; }
        public int Rating { get; set; }   // 1 to 5

        public string Title { get; set; }
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
