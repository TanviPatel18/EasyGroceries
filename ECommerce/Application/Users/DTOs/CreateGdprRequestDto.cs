namespace ECommerce.Application.Users.DTOs
{
    public class CreateGdprRequestDto
    {
        public string CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string RequestType { get; set; }
        public string RequestDetails { get; set; }
    }
}
