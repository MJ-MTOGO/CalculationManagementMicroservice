namespace CalculationManagementService.Application.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get;  set; }
        public Guid CustomerId { get; set; }
        public Guid RestaurantId { get;  set; }
        public decimal TotalPrice { get;  set; }
    }
}
