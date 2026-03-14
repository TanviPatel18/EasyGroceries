namespace ECommerceUI.Models.Users
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CustomerRole { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public string IpAddress { get; set; }
    }
}