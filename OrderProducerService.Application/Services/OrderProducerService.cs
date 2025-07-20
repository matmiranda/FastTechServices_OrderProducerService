
using OrderProducerService.Application.DTOs;
using OrderProducerService.Application.Interfaces;
using OrderProducerService.Infrastructure.MessageBroker;
using OrderProducerService.Infrastructure.Security;

namespace OrderProducerService.Application.Services
{
    public class OrderProducerService : IOrderProducerService
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        private readonly IAuthClient _authClient;

        public OrderProducerService(IRabbitMQProducer rabbitMqProducer, IAuthClient authClient)
        {
            _rabbitMqProducer = rabbitMqProducer;
            _authClient = authClient;
        }

        public async Task PublishOrderAsync(OrderRequest request, string token)
        {
            var isValid = await _authClient.ValidateTokenAsync(token);
            if (!isValid)
                throw new UnauthorizedAccessException("Invalid token");

            await _rabbitMqProducer.PublishAsync("OrderQueue", request);
        }
    }
}
