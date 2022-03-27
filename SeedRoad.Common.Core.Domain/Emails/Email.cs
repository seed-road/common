namespace SeedRoad.Common.Core.Domain.Emails;

public record Email<TContent>(Contact Contact, string Subject, TContent Content) where TContent: EmailContent;