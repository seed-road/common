namespace SeedRoad.Common.Messaging.Configurations;

public interface IUpdateExchange
{
    public string UpdatedRoutingKey { get; set; }
}