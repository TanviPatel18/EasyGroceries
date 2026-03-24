namespace ECommerceUI.Models.Account
{
    public class CreateAddressDto
    {
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string AddressLine1 { get; set; } = "";
        public string? AddressLine2 { get; set; }
        public string? Landmark { get; set; }
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Country { get; set; } = "India";
        public string AddressType { get; set; } = "Home";
        public bool IsDefault { get; set; }
    }
}
