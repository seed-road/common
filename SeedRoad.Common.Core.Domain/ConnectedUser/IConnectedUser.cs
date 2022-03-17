namespace SeedRoad.Common.Core.Domain.ConnectedUser;

public interface IConnectedUser<out TId>
{
    TId Id { get;  }
}