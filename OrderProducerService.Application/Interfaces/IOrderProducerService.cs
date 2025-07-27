using Microsoft.AspNetCore.Mvc;
using OrderProducerService.Application.Request;

namespace OrderProducerService.Application.Interfaces
{
    public interface IOrderProducerService
    {
        Task<IActionResult> PublishOrderCreateAsync(OrderCreateRequest request);
        Task<IActionResult> PublishOrderCancelAsync(OrderCancelRequest request);
    }
}
