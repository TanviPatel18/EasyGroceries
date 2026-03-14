namespace ECommerce.Application.Users.DTOs
{
    public class SearchCustomerDto
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? IpAddress { get; set; }
        public string? CustomerRole { get; set; }

        public DateTime? RegistrationFrom { get; set; }
        public DateTime? RegistrationTo { get; set; }

        public DateTime? LastActivityFrom { get; set; }
        public DateTime? LastActivityTo { get; set; }
    }
}