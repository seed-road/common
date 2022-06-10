namespace SeedRoad.Common.Messaging.Configurations;

public interface IDeleteExchange
{
    public string DeletedRoutingKey { get; set; }
}