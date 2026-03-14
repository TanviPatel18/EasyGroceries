using ECommerce.Application.HomePage.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.HomePage
{
    [ApiController]
    [Route("api/banners")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _bannerService.GetAllAsync();
            return Ok(result);
        }
    }
}