namespace SeedRoad.Common.Presentation.Messaging.Services;

public interface IMessageProcessor<TCommand>
{
    Task<bool> Process(string message);
}