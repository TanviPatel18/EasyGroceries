using ECommerce.Models.Email.Entities;

namespace ECommerce.Models.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        Task CreateAsync(PasswordResetToken token);
        Task<PasswordResetToken?> GetValidTokenAsync(
            string email, string otp);
        Task DeleteByEmailAsync(string email);
        Task MarkAsUsedAsync(string id);
    }
}
