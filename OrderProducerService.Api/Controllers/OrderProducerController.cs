using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderProducerService.Application.DTOs;
using OrderProducerService.Application.Interfaces;

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
        [Authorize]
        public async Task<IActionResult> Post([FromBody] OrderRequest request)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            request.Action = "CREATE"; // <-- Define ação para criação
            await _orderProducerService.PublishOrderAsync(request, token);
            return Ok(new { message = "Pedido enviado com sucesso para a fila." });
        }
    }
}
