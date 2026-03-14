namespace ECommerceUI.Models.Users
{
    public class GdprRequestDto
    {

        public string Id { get; set; } = "";
        public string CustomerId { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public string RequestType { get; set; } = "";
        public string RequestDetails { get; set; } = "";
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "";
        public string ProcessedBy { get; set; } = "";
        public DateTime? ProcessedDate { get; set; }
    }
}
