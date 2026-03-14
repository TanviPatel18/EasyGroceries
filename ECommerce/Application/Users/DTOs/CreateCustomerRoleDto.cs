namespace ECommerce.Application.Users.DTOs
{
    public class CreateCustomerRoleDto
    {
        public string RoleName { get; set; }
        public bool FreeShipping { get; set; }
        public bool TaxExempt { get; set; }
        public bool IsSystemRole { get; set; }
    }
}
