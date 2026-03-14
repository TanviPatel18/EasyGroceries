namespace ECommerceUI.Models.Users
{
    public class VendorDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public string CustomerRoleName { get; set; }
    }

    public class CreateVendorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string CompanyName { get; set; }
    }
}