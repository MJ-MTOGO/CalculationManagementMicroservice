using CalculationManagementService.Application.Ports;
using CalculationManagementService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CalculationManagementService.Infrastructure.Subscribers
{
    public class SubscribeService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public SubscribeService(IMessageBus messageBus, IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync()
        {
            await _messageBus.SubscribeAsync("order-delivered-sub2", async messageData =>
            {
                using var scope = _serviceProvider.CreateScope();
                var calculationService = scope.ServiceProvider.GetRequiredService<CalculationService>();
                try
                {
                    await calculationService.ProcessOrderDeliveredMessageAsync(messageData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            });
        }
    }
}
