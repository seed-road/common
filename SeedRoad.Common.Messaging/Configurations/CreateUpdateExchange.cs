namespace SeedRoad.Common.Messaging.Configurations;

public class CreateUpdateExchange : IExchange, ICreateExchange, IUpdateExchange
{
    public string Name { get; set; } = null!;
    public string CreatedRoutingKey { get; set; } = null!;
    public string UpdatedRoutingKey { get; set; } = null!;
}