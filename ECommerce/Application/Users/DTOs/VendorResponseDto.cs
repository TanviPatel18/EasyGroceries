namespace ECommerce.Application.Users.DTOs
{
    public class VendorResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public string CustomerRoleName { get; set; }
    }
}
