using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SeedRoad.Common.Messaging.Configurations;

namespace SeedRoad.Common.Infrastructure.Messaging.Services
{
    public class DispatcherService : IDispatcherService
    {
        private readonly IModel? _channel;

        private readonly ILogger<DispatcherService> _logger;

        public DispatcherService(IRabbitMqConfiguration configuration,
            ILogger<DispatcherService> logger)
        {
            _logger = logger;

            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = configuration.Host
                };
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                logger.LogError(-1, ex, "RabbitMQClient init fail");
            }

        }

        public void PushMessage(object message, string exchange, string routingKey)
        {
            _logger.LogInformation("PushMessage in {Exchange}, routing key:{RoutingKey}", exchange, routingKey);
            _channel.ExchangeDeclare(exchange, ExchangeType.Topic);

            var msgJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(msgJson);
            _channel.BasicPublish(exchange,
                routingKey,
                null,
                body);
        }
    }
}