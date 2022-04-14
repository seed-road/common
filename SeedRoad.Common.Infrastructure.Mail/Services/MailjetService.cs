using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;
using Microsoft.Extensions.Logging;
using SeedRoad.Common.Core.Domain.Emails;
using SeedRoad.Common.Infrastructure.Mail.Configuration;

namespace SeedRoad.Common.Infrastructure.Mail.Services;

public class MailjetService<TContent> : IEmailSender<TContent> where TContent : EmailContent
{
    private MailjetClient _mailjetClient;
    private ILogger<MailjetService<TContent>> _logger;
    private readonly string _senderEmail;
    private readonly string _senderName;

    public MailjetService(MailjetConfiguration configuration,
        ILogger<MailjetService<TContent>> logger)
    {
        _mailjetClient = new MailjetClient(configuration.ApiKey, configuration.SecretKey);
        _senderEmail = configuration.SenderEmail;
        _senderName = configuration.SenderName;
        _logger = logger;
    }


    public async Task Send(Email<TContent> email)
    {
        TransactionalEmailBuilder msgBuilder = PrepareBuilder(email);
        var response = await _mailjetClient.SendTransactionalEmailAsync(msgBuilder.Build());
        foreach (var responseMessage in response.Messages)
        {
            if (responseMessage.Status == "success")
            {
                _logger.LogInformation("Email sent for: {Email}",
                    GetEmailList(responseMessage));
            }
            else
            {
                _logger.LogInformation("Email fail for : {Email}",
                    GetEmailList(responseMessage));
            }
        }
    }

    private TransactionalEmailBuilder PrepareBuilder(Email<TContent> email)
    {
        var msgBuilder = new TransactionalEmailBuilder()
            .WithFrom(new SendContact(_senderEmail, _senderName))
            .WithSubject(email.Subject)
            .WithTo(new SendContact(email.Contact.Email, email.Contact.Name));
        if (email.Content.CustomId is not null)
        {
            msgBuilder.WithCustomId(email.Content.CustomId);
        }

        if (email.Content.HtmlPart is not null)
        {
            msgBuilder.WithHtmlPart(email.Content.HtmlPart);
        }

        if (email.Content.TextPart is not null)
        {
            msgBuilder.WithTextPart(email.Content.TextPart);
        }

        return msgBuilder;
    }

    private static string GetEmailList(MessageResult responseMessage)
    {
        return string.Join(";", responseMessage.To.Select(r => r.Email).ToList());
    }
}