namespace ECommerce.Application.Sales.DTOs
{
    public class ReturnRequestSearchDto
    {
        public string? OrderId { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
