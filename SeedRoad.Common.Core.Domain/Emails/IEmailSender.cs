namespace SeedRoad.Common.Core.Domain.Emails;

public interface IEmailSender<TContent>
{
    Task Send(Email<TContent> email);
}