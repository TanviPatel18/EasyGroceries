using BCrypt.Net;
using ECommerce.Application.Authentication.DTOs;
using ECommerce.Application.Authentication.Interfaces;
using ECommerce.Application.Email;
using ECommerce.Application.Users.DTOs;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Models.Email.Entities;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;


namespace ECommerce.Application.Authentication.Services
{
    public class AuthService : IAuthService

    {
        private readonly ICustomerRepository _repository;
        private readonly IJwtService _jwtService;
        private readonly ICustomerRoleRepository _roleRepository;
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IEmailService _emailService;

        public AuthService(
            ICustomerRepository repository,
            IJwtService jwtService,
            ICustomerRoleRepository roleRepository,
            IPasswordResetTokenRepository tokenRepository,
            IEmailService emailService)
        {
            _repository = repository;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
            _tokenRepository = tokenRepository;
            _emailService = emailService;
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            var existing = await _repository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new Exception("Email already exists");

            var role = await _roleRepository.GetByNameAsync("Customer");
            if (role == null)
                throw new Exception("Customer role not found");

            var customer = new Customer
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                CustomerRoleId = role.Id,
                CustomerRole = role.RoleName,
                IsActive = true,
                RegistrationDate = DateTime.UtcNow
            };

            await _repository.CreateAsync(customer);

            return "Registered successfully";
        }

        public async Task<(string AccessToken, string RefreshToken, string Role)> LoginAsync(LoginDto dto)
        {
            var customer = await _repository.GetByEmailAsync(dto.Email);

            if (customer == null)
                throw new Exception("Invalid email");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, customer.PasswordHash))
                throw new Exception("Invalid password");

            var accessToken = _jwtService.GenerateToken(customer);
            var refreshToken = GenerateRefreshToken();

            customer.RefreshToken = refreshToken;
            customer.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _repository.UpdateAsync(customer); // ⚠ FIXED (only one parameter)

            return (accessToken, refreshToken, customer.CustomerRole);
        }


        public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var customer = await _repository.GetByRefreshTokenAsync(refreshToken);

            if (customer == null)
                throw new Exception("Invalid refresh token");

            if (customer.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Refresh token expired");

            var newAccessToken = _jwtService.GenerateToken(customer);
            var newRefreshToken = GenerateRefreshToken();

            customer.RefreshToken = newRefreshToken;
            customer.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _repository.UpdateAsync(customer);

            return (newAccessToken, newRefreshToken);
        }
        // ── FORGOT PASSWORD ──
        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            // 1 - find customer by email
            var customer = await _repository.GetByEmailAsync(dto.Email);

            if (customer == null)
                throw new KeyNotFoundException(
                    "No account found with this email.");

            // 2 - check phone matches
            if (customer.Phone != dto.Phone)
                throw new InvalidOperationException(
                    "Email and phone number do not match.");

            // 3 - delete old OTPs
            await _tokenRepository.DeleteByEmailAsync(dto.Email);

            // 4 - generate 6 digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            var token = new PasswordResetToken
            {
                Email = dto.Email,
                Otp = otp,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false
            };

            await _tokenRepository.CreateAsync(token);

            // 5 - build and send OTP email
            var body = "<div style='font-family:Arial,sans-serif;" +
                       "max-width:480px;margin:auto;" +
                       "padding:32px;background:#f5f5f5;'>" +
                       "<h2 style='color:#333;letter-spacing:2px;" +
                       "text-transform:uppercase;'>" +
                       "Password Reset OTP</h2>" +
                       "<p style='color:#666;font-size:15px;'>" +
                       "Use the code below to reset your password. " +
                       "It expires in <strong>10 minutes</strong>.</p>" +
                       "<div style='font-size:42px;font-weight:bold;" +
                       "letter-spacing:12px;color:#333;" +
                       "background:#dcdcdc;padding:24px;" +
                       "text-align:center;margin:24px 0;'>" +
                       otp +
                       "</div>" +
                       "<p style='color:#999;font-size:13px;'>" +
                       "If you did not request this, " +
                       "ignore this email.</p>" +
                       "</div>";

            await _emailService.SendAsync(
                dto.Email,
                "Your Password Reset OTP - Freshora",
                body);
        }


        // ── VERIFY OTP ──
        public async Task VerifyOtpAsync(VerifyOtpDto dto)
        {
            var record = await _tokenRepository
                .GetValidTokenAsync(dto.Email, dto.Otp);

            if (record == null)
                throw new InvalidOperationException(
                    "Invalid or expired OTP.");
        }

        // ── RESET PASSWORD ──
        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            // 1 - validate OTP
            var record = await _tokenRepository
                .GetValidTokenAsync(dto.Email, dto.Otp);

            if (record == null)
                throw new InvalidOperationException(
                    "Invalid or expired OTP.");

            // 2 - hash new password
            var newHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            // 3 - update customer password
            await _repository.UpdatePasswordAsync(dto.Email, newHash);

            // 4 - mark OTP as used
            await _tokenRepository.MarkAsUsedAsync(record.Id);
        }


    }

}
