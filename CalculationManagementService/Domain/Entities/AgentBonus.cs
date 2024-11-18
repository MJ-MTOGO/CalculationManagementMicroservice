using CalculationManagementService.Domain.ValueObjects;
namespace CalculationManagementService.Domain.Entities
{
    public class AgentBonus
    {
        public Guid AgentId { get; private set; }
        public Money Amount { get; private set; }
        public DateTime DeliveringDateTime { get; private set; }
        public Guid OrderId { get; private set; }

        public AgentBonus(Guid agentId, Money amount, DateTime deliveringDateTime, Guid orderId)
        {
            AgentId = agentId;
            Amount = amount;
            DeliveringDateTime = deliveringDateTime;
            OrderId = orderId;
        }
    }
}
