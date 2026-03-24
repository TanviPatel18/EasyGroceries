using ECommerce.Models.Users.Entities;
namespace ECommerce.Application.Users.DTOs
{
    public class UpdateAddressDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; } = "India";
        public AddressType AddressType { get; set; }
        public bool IsDefault { get; set; }
    }
}
