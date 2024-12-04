using CalculationManagementService.Application.DTOs;
using CalculationManagementService.Application.Ports;
using Newtonsoft.Json;

namespace CalculationManagementService.Application.Services
{
    public class CalculationService
    {
        private readonly HttpClient _httpClient;
        private readonly IMessageBus _messageBus;

        public CalculationService(HttpClient httpClient, IMessageBus messageBus)
        {
            _httpClient = httpClient;
            _messageBus = messageBus;
        }

        public async Task ProcessOrderDeliveredMessageAsync(string messageData)
        {
            // Deserialize the DeliveredDto
            var deliveredDto = JsonConvert.DeserializeObject<DeliveredDto>(messageData);
            if (deliveredDto == null)
                throw new Exception("Failed to deserialize DeliveredDto.");

            // Fetch the OrderDto from the API
            var orderResponse = await _httpClient.GetAsync($"http://localhost:5194/api/orders/{deliveredDto.OrderId}");
            orderResponse.EnsureSuccessStatusCode();
            var orderData = await orderResponse.Content.ReadAsStringAsync();
            var orderDto = JsonConvert.DeserializeObject<OrderDto>(orderData);
            if (orderDto == null)
                throw new Exception("Failed to deserialize OrderDto.");

            // Perform earnings calculations
            var earnings = CalculateEarnings(orderDto, deliveredDto);

            // Publish the earnings to the "calculated-earnings" topic
            await _messageBus.PublishAsync("calculated-earnings", earnings);
        }

        private EarningDto CalculateEarnings(OrderDto order, DeliveredDto delivered)
        {
            // Calculate Mtogo earning
            decimal mtogoEarning = CalculateMtogoEarning(order.TotalPrice);

            // Calculate restaurant earning
            decimal restaurantEarning = order.TotalPrice - mtogoEarning;

            // Calculate agent bonus (5% of Mtogo earning before VAT if between 18:00 and 06:00)
            decimal agentBonus = 0;
            if (delivered.DeliveringDatetime.TimeOfDay >= TimeSpan.FromHours(18) || delivered.DeliveringDatetime.TimeOfDay <= TimeSpan.FromHours(6))
            {
                agentBonus = mtogoEarning * 0.05m;
                mtogoEarning = mtogoEarning - agentBonus;

            }

            return new EarningDto
            {
                OrderId = order.OrderId,
                MtogoEarning = mtogoEarning,
                RestaurantEarning = restaurantEarning,
                AgentEarning = agentBonus
            };
        }

        private decimal CalculateMtogoEarning(decimal totalPrice)
        {
            if (totalPrice <= 100) return totalPrice * 0.16m;
            if (totalPrice <= 500) return 100 * 0.16m + (totalPrice - 100) * 0.15m;
            if (totalPrice <= 1000) return 100 * 0.16m + 400 * 0.15m + (totalPrice - 500) * 0.14m;
            return 100 * 0.16m + 400 * 0.15m + 500 * 0.14m + (totalPrice - 1000) * 0.13m;
        }
    }
}
