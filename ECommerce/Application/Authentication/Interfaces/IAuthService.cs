using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Authentication.DTOs;

namespace ECommerce.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
            Task<string> RegisterAsync(RegisterDto dto);

            Task<(string AccessToken, string RefreshToken, string Role)> LoginAsync(LoginDto dto);

            Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);

            Task ForgotPasswordAsync(ForgotPasswordRequestDto dto);
            Task VerifyOtpAsync(VerifyOtpDto dto);
            Task ResetPasswordAsync(ResetPasswordDto dto);
    }
}
