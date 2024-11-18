namespace CalculationManagementService.Application.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message, string topicName);
    }
}
//This interface allows flexibility for different publishing mechanisms.