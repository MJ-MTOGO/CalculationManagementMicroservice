using CalculationManagementService.Application.Interfaces;
using System.Text.Json;

namespace CalculationManagementService.Infrastructure.Publishers
{
    public class PubSubPublisher : IMessagePublisher
    {
        public async Task PublishAsync<T>(T message, string topicName)
        {
            var jsonMessage = JsonSerializer.Serialize(message);

            // Simulate publishing to the topic
            Console.WriteLine($"Published message to {topicName}: {jsonMessage}");
            await Task.CompletedTask;  // Replace with actual publishing code for your pub/sub provider
        }
    }
}
