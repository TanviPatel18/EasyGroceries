using ECommerce.Application.HomePage.DTOs;
using ECommerce.Application.HomePage.Interfaces;
using ECommerce.Models.HomePage;
using ECommerce.Persistence;
using MongoDB.Driver;

namespace ECommerce.Application.HomePage.Service
{
    public class BannerService : IBannerService
    {
        private readonly MongoDbContext _context;

        public BannerService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<BannerDto>> GetAllAsync()
        {
            var banners = await _context.HeroBanners
                .Find(x => x.Published)
                .SortBy(x => x.DisplayOrder)
                .ToListAsync();

            return banners.Select(x => new BannerDto
            {
                Id = x.Id,
                Title = x.Title,
                Subtitle = x.Subtitle,
                ImageUrl = x.ImageUrl,
                ButtonText = x.ButtonText,
                ButtonLink = x.ButtonLink
            }).ToList();
        }
    }
}