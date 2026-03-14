namespace ECommerce.Application.Users.DTOs
{
    public class UpdateCustomerRoleDto
    {
        public string RoleName { get; set; }
        public bool FreeShipping { get; set; }
        public bool TaxExempt { get; set; }
        public bool IsActive { get; set; }
    }
}
