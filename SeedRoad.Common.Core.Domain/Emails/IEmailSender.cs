namespace SeedRoad.Common.Core.Domain.Emails;

public interface IEmailSender<TContent> where TContent : EmailContent
{
    Task Send(Email<TContent> email);
}