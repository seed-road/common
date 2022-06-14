namespace SeedRoad.Common.Messaging.Configurations;

public interface IRabbitMqConfiguration
{
    string Host { get;  }
    string? Username { get;  }
    string? Password { get;  }
    int Port { get;  }
}