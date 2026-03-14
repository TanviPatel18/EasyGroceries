namespace ECommerce.Models.Catalog.Entities
{
    public class ProductImage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMain { get; set; }
    }
}

