namespace SeedRoad.Common.Core.Application.Contracts;

public interface ICurrentUserService<TId>
{
    TId UserId { get; set; }
}