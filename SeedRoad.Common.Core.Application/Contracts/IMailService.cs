namespace SeedRoad.Common.Core.Application.Contracts;

public interface IMailService<TContent>
{
    Task Send(Email<TContent> email);
}