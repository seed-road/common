namespace SeedRoad.Common.Infrastructure.Mail.Configuration;

public class MailjetConfiguration
{
    public string SecretKey { get; set; } = default!;
    public string ApiKey { get; set; } = default!;
    public string SenderEmail { get; set; } = default!;
    public string SenderName { get; set; } = default!;
}