using CalculationManagementService.Application.Interfaces;
using CalculationManagementService.Domain.Aggregates;
using CalculationManagementService.Domain.Entities;
using CalculationManagementService.Domain.ValueObjects;

namespace CalculationManagementService.Application.Services
{
    public class CalculationService
    {
        private readonly IMessagePublisher _publisher;
        private readonly Percentage _mtogoPercentage;
        private readonly Percentage _bonusPercentage;

        public CalculationService(IMessagePublisher publisher)
        {
            _publisher = publisher;
            _mtogoPercentage = new Percentage(15);  // Set default MTOGO fee percentage
            _bonusPercentage = new Percentage(5);   // Set default agent bonus percentage
        }

        public async Task ProcessOrderAsync(Order order)
        {
            // Initialize the OrderAggregate with the received order
            var orderAggregate = new OrderAggregate(order);

            // Perform calculations
            orderAggregate.CalculateEarnings(_mtogoPercentage);
            orderAggregate.CalculateAgentBonus(_bonusPercentage);

            // Publish each result for other services to consume
            await _publisher.PublishAsync(orderAggregate.MTOGOEarning, "mtogo-earnings");
            await _publisher.PublishAsync(orderAggregate.RestaurantEarning, "restaurant-earnings");

            // Publish agent bonus if calculated (e.g., if delivery time is within bonus hours)
            if (orderAggregate.AgentBonus != null)
            {
                await _publisher.PublishAsync(orderAggregate.AgentBonus, "agent-bonuses");
            }

            Console.WriteLine("Results published for Order ID: " + order.Id);
        }
    }
}

//Explanation of CalculationService:
//ProcessOrderAsync accepts the Order object and initializes an OrderAggregate.
//The OrderAggregate calculates the MTOGO earnings, restaurant earnings, and agent bonus.
//Each result is published using _publisher to its respective topic (mtogo-earnings, restaurant-earnings, and agent-bonuses).
