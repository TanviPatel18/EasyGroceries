using System.ComponentModel.DataAnnotations;

namespace ECommerceUI.Models.Auth
{
    public class RegisterModel
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }



        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }


        [Required] public string Password { get; set; }
        [Required] public string ConfirmPassword { get; set; }
    }
}
