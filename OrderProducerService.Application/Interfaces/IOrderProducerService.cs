using OrderProducerService.Application.DTOs;

namespace OrderProducerService.Application.Interfaces
{
    public interface IOrderProducerService
    {
        Task PublishOrderAsync(OrderRequest request, string token);
    }
}
