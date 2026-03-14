namespace ECommerce.Models.Interfaces
{
    public interface ICategoryImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
        Task<bool> DeleteAsync(string imageUrl);
    }
}
