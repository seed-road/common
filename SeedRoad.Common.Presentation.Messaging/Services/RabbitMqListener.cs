using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SeedRoad.Common.Messaging.Configurations;

namespace SeedRoad.Common.Presentation.Messaging.Services
{
    public class RabbitMqListener<TEvent> : IHostedService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly RoutingConfiguration _routingConfiguration;
        private readonly IMessageProcessor<TEvent> _messageProcessor;
        private readonly ILogger<RabbitMqListener<TEvent>> _logger;


        public RabbitMqListener(IRabbitMqConfiguration configuration, RoutingConfiguration routingConfiguration,
            IMessageProcessor<TEvent> messageProcessor,
            ILogger<RabbitMqListener<TEvent>> logger)
        {
            _routingConfiguration = routingConfiguration;
            _messageProcessor = messageProcessor;
            _logger = logger;

            try
            {
                _logger.LogDebug(
                    "RabbitMqListener connecting to host :  {Host}, password: {Password}, username: {Username}, port: {Port}",
                    configuration.Host, configuration.Password, configuration.Username, configuration.Port);
                var factory = new ConnectionFactory
                {
                    HostName = configuration.Host
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                _logger.LogError("RabbitListener init error,ex:{Error}", ex.Message);
                throw;
            }
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Register();
            return Task.CompletedTask;
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            return Task.CompletedTask;
        }

        // Registered consumer monitoring here
        public void Register()
        {
            try
            {
                _channel.ExchangeDeclare(_routingConfiguration.Exchange, ExchangeType.Topic);
                var queueName = _channel.QueueDeclare().QueueName;
                _logger.LogInformation(
                    "RabbitListener register, exchange: {Exchange}, routeKey:{RoutingKey}, queueName {QueueName}",
                    _routingConfiguration.Exchange, _routingConfiguration.RoutingKey, queueName);

                _channel.QueueBind(queueName,
                    _routingConfiguration.Exchange,
                    _routingConfiguration.RoutingKey);
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());

                    var result = await _messageProcessor.Process(message);
                    if (result)
                    {
                        _channel.BasicAck(ea.DeliveryTag, false);
                    }
                };
                _channel.BasicConsume(queue: queueName, consumer: consumer);
            }
            catch (Exception exception)
            {
                _logger.LogError("RabbitMQ error:{Error}", exception.Message);
            }
        }

        public void UnRegister()
        {
            _connection.Close();
        }
    }
}