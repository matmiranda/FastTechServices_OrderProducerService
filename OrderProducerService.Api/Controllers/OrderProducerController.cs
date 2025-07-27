using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderProducerService.Application.Interfaces;
using OrderProducerService.Application.Request;

namespace MenuProducerService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderProducerController : ControllerBase
    {
        private readonly IOrderProducerService _orderProducerService;

        public OrderProducerController(IOrderProducerService orderProducerService)
        {
            _orderProducerService = orderProducerService;
        }

        [HttpPost]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> CriarPedido([FromBody] OrderCreateRequest request)
        {
            return await _orderProducerService.PublishOrderCreateAsync(request);
        }

        [HttpPatch("cancel")]
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> CancelOrder([FromBody] OrderCancelRequest request)
        {
            return await _orderProducerService.PublishOrderCancelAsync(request);
        }
    }
}
