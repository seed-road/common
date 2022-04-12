using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeedRoad.Common.Configuration;
using SeedRoad.Common.Core.Domain.Emails;
using SeedRoad.Common.Infrastructure.Mail.Services;
using SeedRoad.IdentityManagement.Infrastructure.Mail.Configuration;

namespace SeedRoad.Common.Infrastructure.Mail;

public static class DependencyInjection
{
    public static IServiceCollection InjectMail(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var mailjetConfiguration = configuration.GetRequiredConfiguration<MailjetConfiguration>();
        return serviceCollection
            .AddSingleton(mailjetConfiguration)
            .AddScoped(typeof(IEmailSender<>), typeof(MailjetService<>));
    }
}