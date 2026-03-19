using System.ComponentModel.DataAnnotations;

namespace ECommerceUI.Models.Auth
{
    public class ResetPasswordModel
    {
        public string Email { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
