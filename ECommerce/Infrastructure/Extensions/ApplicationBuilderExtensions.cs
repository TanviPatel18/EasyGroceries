using ECommerce.Persistence;
using ECommerce.Models.Users.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void SeedDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

            var roles = context.CustomerRoles;
            var roleIndex = new CreateIndexModel<CustomerRole>(
                 Builders<CustomerRole>.IndexKeys.Ascending(r => r.RoleName),
                 new CreateIndexOptions { Unique = true });

            roles.Indexes.CreateOne(roleIndex);
            var emailIndex = new CreateIndexModel<Customer>(
                Builders<Customer>.IndexKeys.Ascending(c => c.Email),
                new CreateIndexOptions { Unique = true });

            //customers.Indexes.CreateOne(emailIndex);


            var customers = context.Customers;

            // ✅ 1. Ensure Roles Exist
            var roleNames = new[] { "Admin", "Vendor", "Customer" };

            foreach (var roleName in roleNames)
            {
                if (!roles.Find(r => r.RoleName == roleName).Any())
                {
                    roles.InsertOne(new CustomerRole
                    {
                        RoleName = roleName,
                        IsActive = true,
                        IsDeleted = false,   // 🔥 THIS WAS MISSING
                        IsSystemRole = roleName == "Admin",
                        FreeShipping = false,
                        TaxExempt = false,
                        CreatedOn = DateTime.UtcNow
                    });
                }
            }

            // ✅ 2. Get Admin Role
            var adminRole = roles.Find(r => r.RoleName == "Admin").FirstOrDefault();

            if (adminRole == null)
                return; // safety check

            // ✅ 3. Ensure Admin User Exists
            if (!customers.Find(c => c.Email == "admin@ecommerce.com").Any())
            {
                customers.InsertOne(new Customer
                {
                    Email = "admin@ecommerce.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    FirstName = "System",
                    LastName = "Admin",
                    CustomerRoleId = adminRole.Id,
                    CustomerRole = adminRole.RoleName,
                    IsActive = true,
                    RegistrationDate = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow
                });
            }
        }
    }
}
