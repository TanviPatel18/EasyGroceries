using System.ComponentModel.DataAnnotations;

namespace ECommerceUI.Models.Users
{
    public class CreateCustomerRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
        public bool FreeShipping { get; set; }
        public bool TaxExempt { get; set; }
        public bool IsSystemRole { get; set; }
    }
}
