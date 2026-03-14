using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Users.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Models.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(string id);
        Task<Customer> GetByEmailAsync(string email);
        Task CreateAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task SoftDeleteByEmailAsync(string email);
        Task<Customer> GetByRefreshTokenAsync(string refreshToken);

    }
}
