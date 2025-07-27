using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProducerService.Application.Interfaces;
using OrderProducerService.Application.Request;
using OrderProducerService.Application.Response;
using OrderProducerService.Infrastructure.MessageBroker;
using OrderProducerService.Infrastructure.Security;

namespace OrderProducerService.Application.Services
{
    public class OrderProducerService : IOrderProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string MessageSent = "Item enviado com sucesso para a fila.";

        public OrderProducerService(
            IRabbitMQProducer rabbitMqProducer, 
            IAuthClient authClient, 
            IHttpContextAccessor httpContextAccessor)
        {
            _rabbitMqProducer = rabbitMqProducer;
            _authClient = authClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> PublishOrderCreateAsync(OrderCreateRequest request)
        {
            await ValidateTokenAsync();

            await _rabbitMqProducer.PublishAsync(QueueNames.OrderPlaced, request);

            return new OkObjectResult(new OrderResponse
            {
                Message = MessageSent
            });
        }

        public async Task<IActionResult> PublishOrderCancelAsync(OrderCancelRequest request)
        {
            //await ValidateTokenAsync();

            await _rabbitMqProducer.PublishAsync(QueueNames.OrderCancelled, request);

            return new OkObjectResult(new OrderResponse
            {
                Message = MessageSent
            });
        }

        public async Task ValidateTokenAsync()
        {
            var headers = _httpContextAccessor.HttpContext?.Request?.Headers;

            if (headers == null || !headers.TryGetValue("Authorization", out var token))
                throw new UnauthorizedAccessException("Token não encontrado no header.");

            if (!token.ToString().StartsWith("Bearer "))
                throw new UnauthorizedAccessException("Formato inválido do token.");

            var cleanToken = token.ToString().Replace("Bearer ", "");
            var isValid = await _authClient.ValidateTokenAsync(cleanToken);

            if (!isValid)
                throw new UnauthorizedAccessException("Token inválido.");
        }
    }
}
