namespace SeedRoad.Common.Application.Contracts;

public interface ICurrentUserService<TId>
{
    TId UserId { get; set; }
}