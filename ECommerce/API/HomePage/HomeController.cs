using ECommerce.Application.HomePage.Interfaces;
using ECommerce.Persistence;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ECommerce.API.HomePage
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        private readonly MongoDbContext _context;

        public HomeController(IBannerService bannerService, MongoDbContext context)
        {
            _bannerService = bannerService;
            _context = context;
        }

        [HttpGet("banners")]
        public async Task<IActionResult> GetBanners()
        {
            var banners = await _bannerService.GetAllAsync();
            return Ok(banners);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories
                .Find(x => x.Published)
                .SortBy(x => x.DisplayOrder)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("featured-products")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var products = await _context.Products
                .Find(x => x.Published)
                .Limit(8)
                .ToListAsync();

            return Ok(products);
        }
    }
}