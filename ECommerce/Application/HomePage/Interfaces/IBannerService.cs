using ECommerce.Application.HomePage.DTOs;
using ECommerce.Models.HomePage;

namespace ECommerce.Application.HomePage.Interfaces
{
    public interface IBannerService
    {
        Task<List<BannerDto>> GetAllAsync();
    }
}
