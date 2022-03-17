namespace SeedRoad.Common.Core.Application.Contracts;

public record Email<T>(Contact Contact, string Subject, T Content);