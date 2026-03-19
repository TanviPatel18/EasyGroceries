using ECommerce.Models.Email.Entities;
using ECommerce.Models.Interfaces;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Infrastructure.Repositories
{
    public class PasswordResetTokenRepository
       : IPasswordResetTokenRepository
    {
        private readonly MongoDbContext _db;

        public PasswordResetTokenRepository(MongoDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(PasswordResetToken token)
        {
            await _db.PasswordResetTokens.InsertOneAsync(token);
        }

        public async Task<PasswordResetToken?> GetValidTokenAsync(
            string email, string otp)
        {
            return await _db.PasswordResetTokens
                .Find(t => t.Email == email &&
                           t.Otp == otp &&
                           !t.IsUsed &&
                           t.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteByEmailAsync(string email)
        {
            await _db.PasswordResetTokens
                .DeleteManyAsync(t => t.Email == email);
        }

        public async Task MarkAsUsedAsync(string id)
        {
            await _db.PasswordResetTokens.UpdateOneAsync(
                t => t.Id == id,
                Builders<PasswordResetToken>.Update
                    .Set(t => t.IsUsed, true));
        }
    }
}
