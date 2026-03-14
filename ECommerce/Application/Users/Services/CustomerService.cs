using ECommerce.Application.Users.DTOs;
using ECommerce.Application.Users.Interfaces;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Users.Entities;
using ECommerce.Persistence;
using MongoDB.Driver;


namespace ECommerce.Application.Users.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(MongoDbContext context)
        {
            _customers = context.Customers;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customers
                .Find(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _customers
                .Find(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> CreateAsync(CreateCustomerDto dto)
        {
           
            // 🔹 Check if email already exists
            var existing = await _customers
                .Find(c => c.Email == dto.Email && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (existing != null)
                throw new Exception("Email already exists");

            // 🔹 Hash password (IMPORTANT)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var customer = new Customer
            {
                Email = dto.Email,
                PasswordHash = hashedPassword, // store hashed password
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                CustomerRole = dto.CustomerRole,
                IsTaxExempt = dto.IsTaxExempt,
                IsActive = dto.IsActive,
                Newsletter = dto.Newsletter,
                Vendor = dto.Vendor,
                AdminComment = dto.AdminComment,
                RegistrationDate = DateTime.UtcNow,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await _customers.InsertOneAsync(customer);

            return customer;
        }

        public async Task<List<Customer>> SearchAsync(SearchCustomerDto dto)
        {
            // Get all non-deleted customers first
            var customers = await _customers
                .Find(c => !c.IsDeleted)
                .ToListAsync();

            // Email
            if (!string.IsNullOrWhiteSpace(dto.Email))
                customers = customers
                    .Where(c => c.Email != null &&
                                c.Email.ToLower().Contains(dto.Email.ToLower()))
                    .ToList();

            // First Name
            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                customers = customers
                    .Where(c => c.FirstName != null &&
                                c.FirstName.ToLower().Contains(dto.FirstName.ToLower()))
                    .ToList();

            // Last Name
            if (!string.IsNullOrWhiteSpace(dto.LastName))
                customers = customers
                    .Where(c => c.LastName != null &&
                                c.LastName.ToLower().Contains(dto.LastName.ToLower()))
                    .ToList();

            // Phone
            if (!string.IsNullOrWhiteSpace(dto.Phone))
                customers = customers
                    .Where(c => c.Phone != null &&
                                c.Phone.ToLower().Contains(dto.Phone.ToLower()))
                    .ToList();

            // IP Address
            if (!string.IsNullOrWhiteSpace(dto.IpAddress))
                customers = customers
                    .Where(c => c.IpAddress != null &&
                                c.IpAddress.ToLower().Contains(dto.IpAddress.ToLower()))
                    .ToList();

            // Customer Role
            if (!string.IsNullOrWhiteSpace(dto.CustomerRole))
                customers = customers
                    .Where(c => c.CustomerRole != null &&
                                c.CustomerRole.ToLower().Contains(dto.CustomerRole.ToLower()))
                    .ToList();

            // Registration Date From
            if (dto.RegistrationFrom.HasValue)
                customers = customers
                    .Where(c => c.RegistrationDate >= dto.RegistrationFrom.Value)
                    .ToList();

            // Registration Date To
            if (dto.RegistrationTo.HasValue)
                customers = customers
                    .Where(c => c.RegistrationDate <= dto.RegistrationTo.Value)
                    .ToList();

            // Last Activity From
            if (dto.LastActivityFrom.HasValue)
                customers = customers
                    .Where(c => c.LastActivityDate.HasValue &&
                                c.LastActivityDate.Value >= dto.LastActivityFrom.Value)
                    .ToList();

            // Last Activity To
            if (dto.LastActivityTo.HasValue)
                customers = customers
                    .Where(c => c.LastActivityDate.HasValue &&
                                c.LastActivityDate.Value <= dto.LastActivityTo.Value)
                    .ToList();

            return customers;
        }

        public async Task UpdateByEmailAsync(string email, UpdateCustomerDto dto)
        {
            var customer = await _customers
                 .Find(c => c.Email == email && !c.IsDeleted)
                 .FirstOrDefaultAsync();

            if (customer == null)
                throw new Exception("Customer not found");

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Phone = dto.Phone;
            customer.UpdatedOn = DateTime.UtcNow;

            await _customers.ReplaceOneAsync(
                    c => c.Id == customer.Id,
                    customer
            );
        }
        public async Task SoftDeleteByEmailAsync(string email)
        {
            var customer = await _customers
                .Find(c => c.Email == email && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (customer == null)
                throw new Exception("Customer not found");

            var update = Builders<Customer>.Update
                .Set(c => c.IsDeleted, true)
                .Set(c => c.UpdatedOn, DateTime.UtcNow);

            await _customers.UpdateOneAsync(
                c => c.Email == email,
                update
            );
        }
    }
}
