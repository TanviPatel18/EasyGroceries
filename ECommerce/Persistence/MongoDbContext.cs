using ECommerce.Models.Catalog.Entities;
using ECommerce.Models.Email.Entities;
using ECommerce.Models.Email.Entities;
using ECommerce.Models.HomePage;
using ECommerce.Models.Sales.Entities;
using ECommerce.Models.Users.Entities;
using ECommerce.Models.Wishlist.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<Category> Categories =>
            _db.GetCollection<Category>("Categories");

        public IMongoCollection<Product> Products =>
            _db.GetCollection<Product>("Products");

        public IMongoCollection<Manufacturer> Manufacturers =>
            _db.GetCollection<Manufacturer>("Manufacturers");

        public IMongoCollection<Customer> Customers =>
            _db.GetCollection<Customer>("Customers");

        public IMongoCollection<CustomerRole> CustomerRoles =>
             _db.GetCollection<CustomerRole>("CustomerRoles");

        public IMongoCollection<Vendor> Vendors =>
            _db.GetCollection<Vendor>("Vendors");

        public IMongoCollection<OnlineCustomer> OnlineCustomers =>
            _db.GetCollection<OnlineCustomer>("OnlineCustomers");

        public IMongoCollection<GdprRequestLog> GdprRequests =>
            _db.GetCollection<GdprRequestLog>("GdprRequests");

        public IMongoCollection<ShoppingCart> ShoppingCarts =>
            _db.GetCollection<ShoppingCart>("ShoppingCarts");

        public IMongoCollection<ShoppingCartItem> ShoppingCartItems =>
            _db.GetCollection<ShoppingCartItem>("ShoppingCartItems");

        public IMongoCollection<Order> Orders =>
            _db.GetCollection<Order>("Orders");

        public IMongoCollection<OrderItem> OrderItems =>
            _db.GetCollection  <OrderItem>("OrderItems");

        public IMongoCollection<Shipment> Shipments =>
            _db.GetCollection<Shipment>("Shipments");

        public IMongoCollection<RecurringPayment> RecurringPayments =>
            _db.GetCollection<RecurringPayment>("RecurringPayments");

        public IMongoCollection<ReturnRequest> ReturnRequests =>
            _db.GetCollection<ReturnRequest>("ReturnRequests");

        public IMongoCollection<HeroBanner> HeroBanners =>
            _db.GetCollection<HeroBanner>("HeroBanners");

        public IMongoCollection<WishlistItem> Wishlists =>
      _db.GetCollection<WishlistItem>("Wishlists");

        public IMongoCollection<PasswordResetToken> PasswordResetTokens =>
            _db.GetCollection<PasswordResetToken>("PasswordResetTokens");


        public IMongoCollection<CustomerAddress> CustomerAddresses =>
    _db.GetCollection<CustomerAddress>("CustomerAddresses");
    }
}
