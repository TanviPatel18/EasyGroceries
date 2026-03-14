namespace ECommerceUI.Models.Catalog
{
    public class CategoryVm
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
    }
}
