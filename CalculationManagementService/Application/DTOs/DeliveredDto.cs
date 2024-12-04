namespace CalculationManagementService.Application.DTOs
{
    public class DeliveredDto
    {
        public Guid OrderId { get; set; }
        public Guid AgentId { get; set; }
        public DateTime DeliveringDatetime { get; set; }
    }
}
