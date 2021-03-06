using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeedRoad.Common.Presentation.Messaging.Services
{
    public class MessageProcessor<TEvent> : IMessageProcessor<TEvent>
    {
        private readonly ILogger<MessageProcessor<TEvent>> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MessageProcessor(ILogger<MessageProcessor<TEvent>> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> Process(string message)
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<ISender>();
                JObject json = JObject.Parse(message);
                var runParticipation = JsonConvert.DeserializeObject<TEvent>(json.ToString());
                if (runParticipation is null)
                {
                    _logger.LogError("Cannot parse rabbitmq message");
                    return false;
                }

                _logger.LogInformation("Process  message {Message}", message);

                await sender.Send(runParticipation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Process fail, error:{ExceptionMessage},stackTrace:{ex.StackTrace},message:{Message}",
                    ex.Message, ex.StackTrace, message);
                _logger.LogError(-1, ex, "Process fail");
                return false;
            }
        }
    }
}