using CalculationManagementService.Application.DTOs;

namespace CalculationManagementService.Application.Ports
{
    public interface EarningPublisher
    {
     
            Task PublishErningAsync(EarningDto earningDto);
        
    }   
}
    