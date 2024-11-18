using CalculationManagementService.Application.Interfaces;
using CalculationManagementService.Application.Services;
using CalculationManagementService.Domain.Entities;
using System.Text.Json;

namespace CalculationManagementService.Infrastructure.Subscribers
{
    public class OrderSubscriber : IOrderSubscriber
    {
        private readonly CalculationService _calculationService;

        public OrderSubscriber(CalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        public async Task SubscribeAsync()
        {
            Console.WriteLine("Starting subscription to the orders topic...");

            // Simulate receiving a message from the pub/sub topic
            // Replace this with actual code to subscribe to a real message broker (like Google Pub/Sub)
            while (true)
            {
                // Simulated JSON order message (replace this with actual message data)
                var jsonOrderMessage = "{ \"Id\": \"some-guid\", \"TotalPrice\": 300.00, /* other fields */ }";

                // Deserialize the JSON message into an Order object
                var order = JsonSerializer.Deserialize<Order>(jsonOrderMessage);

                if (order != null)
                {
                    Console.WriteLine("Received order message. Processing...");
                    await _calculationService.ProcessOrderAsync(order);
                }

                await Task.Delay(5000); // Polling interval (remove if using a real message broker)
            }
        }
    }
}
