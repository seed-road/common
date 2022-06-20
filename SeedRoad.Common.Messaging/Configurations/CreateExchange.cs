namespace SeedRoad.Common.Messaging.Configurations;

public class CreateExchange : IExchange, ICreateExchange
{
    public string Name { get; set; } = null!;
    public string CreatedRoutingKey { get; set; } = null!;
}