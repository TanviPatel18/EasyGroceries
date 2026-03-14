using System.ComponentModel.DataAnnotations;

namespace ECommerceUI.Models.Users
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be 10 digits")]
        public string Phone { get; set; }

        public bool IsTaxExempt { get; set; }

        public string Newsletter { get; set; }

        [Required(ErrorMessage = "Customer role is required")]
        public string CustomerRole { get; set; }

        public string Vendor { get; set; }

        public bool IsActive { get; set; } = true;

        public string AdminComment { get; set; }
    }
}