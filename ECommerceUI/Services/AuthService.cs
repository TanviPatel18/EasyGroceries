using System.Net;
using System.Net.Http.Json;
using ECommerceUI.Models.Auth;

namespace ECommerceUI.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        // ── REGISTER ──
        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var response = await _http
                .PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }

        // ── LOGIN ──
        public async Task<LoginResponse?> LoginAsync(LoginModel model)
        {
            var response = await _http
                .PostAsJsonAsync("api/auth/login", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<LoginResponse>();
        }

        // ── FORGOT PASSWORD ──
        public async Task<string> ForgotPasswordAsync(
            ForgotPasswordModel model)
        {
            var response = await _http
                .PostAsJsonAsync("api/auth/forgot-password", model);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return "not_found";

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return "mismatch";

            return "ok";
        }

        // ── VERIFY OTP ──
        public async Task<string> VerifyOtpAsync(VerifyOtpModel model)
        {
            var response = await _http
                .PostAsJsonAsync("api/auth/verify-otp", model);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return "invalid";

            return "ok";
        }

        // ── RESET PASSWORD ──
        public async Task<string> ResetPasswordAsync(
            ResetPasswordModel model)
        {
            var response = await _http
                .PostAsJsonAsync("api/auth/reset-password", model);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return "invalid";

            return response.IsSuccessStatusCode ? "ok" : "error";
        }
    }
}