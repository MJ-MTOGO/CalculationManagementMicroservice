using CalculationManagementService.Domain.Aggregates;
using CalculationManagementService.Domain.Entities;
using CalculationManagementService.Domain.ValueObjects;

namespace CalculationManagementServiceTests.UnitTest
{
    public class CalculationTest
    {


        [Fact]
        public async Task MTOGOEarning()
        {

            // Arrange
            Order order = new Order(
                id: Guid.NewGuid(),
                restaurantId: Guid.NewGuid(),
                totalPrice: 125,
                orderDateTime: DateTime.Now,
                deliveringDateTime: DateTime.Now,
                customerId: Guid.NewGuid(),
                deliveringAddress: "123"
            );
            OrderAggregate aggregate = new OrderAggregate(order);

            // Act
            var mtogoPercentage = new Percentage(15); // Assuming 15% is the MTOGO percentage
            aggregate.CalculateEarnings(mtogoPercentage);

            var expectedResult = (order.TotalPrice / 100) * 15;

            Assert.Equal(expectedResult, aggregate.MTOGOEarning.Price.Amount);
        }


        [Fact]
        public void RestaurantEarning()
        {
            // Arrange
            Order order = new Order(
                id: Guid.NewGuid(),
                restaurantId: Guid.NewGuid(),
                totalPrice: 125,
                orderDateTime: DateTime.Now,
                deliveringDateTime: DateTime.Now,
                customerId: Guid.NewGuid(),
                deliveringAddress: "123"
            );
            OrderAggregate aggregate = new OrderAggregate(order);

            // Act
            var mtogoPercentage = new Percentage(15); // Assuming 15% is the MTOGO percentage
            aggregate.CalculateEarnings(mtogoPercentage);

            // Assert
            var expectedEarning = order.TotalPrice - ((order.TotalPrice / 100) * 15);
            Assert.Equal(expectedEarning, aggregate.RestaurantEarning.Price.Amount);
        }


        [Fact]
        public async Task AgentBonus()
        {

            // Arrange
            Order order = new Order(
                id: Guid.NewGuid(),
                restaurantId: Guid.NewGuid(),
                totalPrice: 125,
                orderDateTime: DateTime.Now.Date.AddHours(19), // Sets to 19:00 (7 PM)
                deliveringDateTime: DateTime.Now.Date.AddHours(20),
                customerId: Guid.NewGuid(),
                deliveringAddress: "123"
            );
            OrderAggregate aggregate = new OrderAggregate(order);

            // Act
            var mtogoPercentage = new Percentage(15); // Assuming 15% is the MTOGO percentage
            aggregate.CalculateEarnings(mtogoPercentage);
            var agentPercentage = new Percentage(5); // Assuming 15% is the Agent percentage
            aggregate.CalculateAgentBonus(agentPercentage);

            // Assert
            var mtogoEarning = ((order.TotalPrice / 100) * 15);
            var expectedEarning = (mtogoEarning / 100) * 5;

            Assert.Equal(expectedEarning, aggregate.AgentBonus.Price.Amount);
        }
    }
}   