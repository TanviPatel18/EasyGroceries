namespace ECommerce.Application.Sales.DTOs
{
    public class OrderAddressDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }   // ← NEW
        public string ZipCode { get; set; }
        public string PaymentMethod { get; set; }
    }

}
