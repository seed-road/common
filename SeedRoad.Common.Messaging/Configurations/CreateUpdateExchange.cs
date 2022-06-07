namespace SeedRoad.Common.Messaging.Configurations;

public class CreateUpdateExchange : IExchange
{
    public string Name { get; set; } = null!;
    public string CreatedRoutingKey { get; set; } = null!;
    public string UpdatedRoutingKey { get; set; } = null!;
}
