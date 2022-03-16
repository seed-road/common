namespace SeedRoad.Common.Messaging.Configurations;

public class RabbitMqConfiguration : IRabbitMqConfiguration
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
}