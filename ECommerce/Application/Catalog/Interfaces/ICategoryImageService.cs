namespace ECommerce.Application.Catalog.Interfaces
{
    public interface ICategoryImageService
    {
        Task<string> UploadAsync(IFormFile file);
        Task<bool> DeleteAsync(string imageUrl);
    }
}
