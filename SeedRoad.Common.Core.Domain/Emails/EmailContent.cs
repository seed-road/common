namespace SeedRoad.Common.Core.Domain.Emails;

public abstract class EmailContent
{
    public virtual string? TextPart { get; }
    public virtual string? HtmlPart { get; }
    public virtual string? CustomId { get; }
}