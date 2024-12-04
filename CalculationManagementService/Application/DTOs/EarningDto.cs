namespace CalculationManagementService.Application.DTOs
{
    public class EarningDto
    {
        public Guid OrderId { get; set; }
        public decimal MtogoEarning { get; set; }
        public decimal RestaurantEarning { get; set; }
        public decimal AgentEarning{ get; set; }
    }
}
