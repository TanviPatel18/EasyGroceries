using BCrypt.Net;
using ECommerce.Application.Authentication.Interfaces;
using ECommerce.Application.Users.DTOs;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;


namespace ECommerce.Application.Authentication.Services
{
    public class AuthService : IAuthService

    {
        private readonly ICustomerRepository _repository;
        private readonly IJwtService _jwtService;
        private readonly ICustomerRoleRepository _roleRepository;

        public AuthService(ICustomerRepository repository, IJwtService jwtService, ICustomerRoleRepository roleRepository)
        {
            _repository = repository;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
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


    }

}
