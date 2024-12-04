using CalculationManagementService.Application.Services;
using CalculationManagementService.Application.Ports;

namespace CalculationManagementService.Infrastructure.Subscribers
{
    public class SubscribeService
    {
        private readonly IMessageBus _messageBus;
        private readonly CalculationService _calculationService;

        public SubscribeService(IMessageBus messageBus, CalculationService calculationService)
        {
            _messageBus = messageBus;
            _calculationService = calculationService;
        }

        public async Task StartAsync()
        {
            await _messageBus.SubscribeAsync("order-delivered-sub", async messageData =>
            {
                try
                {
                    await _calculationService.ProcessOrderDeliveredMessageAsync(messageData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            });
        }
    }
}
