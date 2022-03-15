namespace SeedRoad.Common.Presentation.Messaging.Services;

public interface IMessageProcessor<TCommand>
{
    bool Process(string message);
}