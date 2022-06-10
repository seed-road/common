namespace SeedRoad.Common.Messaging.Configurations;

public interface ICreateExchange
{
    public string CreatedRoutingKey { get; set; }
}