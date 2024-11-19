using CalculationManagementService.Domain.ValueObjects;
namespace CalculationManagementService.Domain.Entities
{
    public class MTOGOEarning
    {
        public Guid OrderId { get; private set; }
        public Money Price { get; private set; }
        public DateTime DateTime { get; private set; }

        public MTOGOEarning(Guid orderId, Money amount)
        {
            OrderId = orderId;
            Price = amount;
            DateTime = DateTime.Now;
        }
    }
}
