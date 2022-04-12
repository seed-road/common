namespace SeedRoad.IdentityManagement.Infrastructure.Mail.Configuration;

public class MailjetConfiguration
{
    public string SecretKey { get; set; }
    public string ApiKey { get; set; }
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }
}