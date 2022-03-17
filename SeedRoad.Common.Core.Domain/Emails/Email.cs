namespace SeedRoad.Common.Core.Domain.Emails;

public record Email<T>(Contact Contact, string Subject, T Content);