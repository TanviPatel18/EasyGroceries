using System.ComponentModel.DataAnnotations;

namespace ECommerceUI.Models.Auth
{
    public class ForgotPasswordModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = string.Empty;
    }
}
