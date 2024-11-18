namespace CalculationManagementService.Application.Interfaces
{
    public interface IOrderSubscriber
    {
        Task SubscribeAsync();
    }
}


//This class will listen to the orders topic,
//receive the message, deserialize it into an Order object,
//and send it to CalculationService for processing.
