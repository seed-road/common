namespace SeedRoad.Common.Messaging.Configurations;

public class RoutingConfiguration
{
    public RoutingConfiguration(string routingKey, string exchange)
    {
        if (!routingKey.StartsWith($"{exchange}."))
        {
            routingKey = $"{exchange}.{routingKey}";
        }
        RoutingKey = routingKey;
        Exchange = exchange;
    }

    public string RoutingKey { get; set; }
    public string Exchange { get; set; }
}