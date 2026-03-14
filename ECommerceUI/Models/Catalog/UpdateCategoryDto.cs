namespace ECommerceUI.Models.Catalog
{
    public class UpdateCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public string ImageUrl { get; set; }
    }

}
