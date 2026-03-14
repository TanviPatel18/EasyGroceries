using ECommerce.Models.Users.Entities;
using ECommerce.Models.Users;

namespace ECommerce.Application.Authentication.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Customer customer);
    }
}
