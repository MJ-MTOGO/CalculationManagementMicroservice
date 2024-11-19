using CalculationManagementService.Domain.Entities;
using CalculationManagementService.Domain.ValueObjects;

namespace CalculationManagementService.Domain.Aggregates
{
    public class OrderAggregate
    {
        public Order Order { get; private set; }
        public MTOGOEarning MTOGOEarning { get; private set; }
        public RestaurantEarning RestaurantEarning { get; private set; }
        public AgentBonus AgentBonus { get; private set; }

        public OrderAggregate(Order order)
        {
            Order = order;
        }

        public void CalculateEarnings(Percentage mtogoPercentage)
        {
            if (mtogoPercentage != null)
            {
                // Calculate MTOGO earning based on percentage
                var mtogoEarningAmount = mtogoPercentage.ApplyTo(new Money(Order.TotalPrice, "DKK"));
                MTOGOEarning = new MTOGOEarning(Order.Id, mtogoEarningAmount);

                // Calculate Restaurant Earning
                Decimal t = (decimal)106.25;
                var restaurantEarningAmount = new Money(Order.TotalPrice - mtogoEarningAmount.Amount, "DKK");
 
                RestaurantEarning = new RestaurantEarning(Order.Id,restaurantEarningAmount );
            }
        }

        public void CalculateAgentBonus(Percentage bonusPercentage)
        {
            if (bonusPercentage != null)
            {
                // Check if the delivery time is between 18:00 and 06:00
                if (Order.DeliveringDateTime.Hour >= 18 || Order.DeliveringDateTime.Hour < 6)
                {
                    var bonusAmount = bonusPercentage.ApplyTo(MTOGOEarning.Price);
                    AgentBonus = new AgentBonus(Guid.NewGuid(), bonusAmount, Order.DeliveringDateTime, Order.Id);
                }
            }
        }

    }
}
