using ECommerce.Application.Users.DTOs;

namespace ECommerce.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
            Task<string> RegisterAsync(RegisterDto dto);

            Task<(string AccessToken, string RefreshToken)> LoginAsync(LoginDto dto);

            Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
        

    }
}
