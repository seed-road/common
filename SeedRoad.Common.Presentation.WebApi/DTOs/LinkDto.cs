namespace SeedRoad.Common.Presentation.WebApi.DTOs;

public class LinkDto
{
    public LinkDto(string? href, string rel, HttpMethod method)
    {
        Href = href ?? throw new ArgumentException($"Link href cannot be null for new {nameof(LinkDto)} instance");
        Rel = rel;
        Method = method.Method;
    }

    public string Href { get; }
    public string Rel { get; }
    public string Method { get; }

    public static LinkDto GetLink(string? href, string rel)
    {
        return new LinkDto(href, rel, HttpMethod.Get);
    }

    public static LinkDto PostLink(string? href, string rel)
    {
        return new LinkDto(href, rel, HttpMethod.Post);
    }

    public static LinkDto PutLink(string? href, string rel)
    {
        return new LinkDto(href, rel, HttpMethod.Put);
    }

    public static LinkDto DeleteLink(string? href, string rel)
    {
        return new LinkDto(href, rel, HttpMethod.Delete);
    }


    public static LinkDto SelfLink(string? href)
    {
        return new LinkDto(href, "self", HttpMethod.Get);
    }

    public static LinkDto AuthLink(string? href)
    {
        return new LinkDto(href, "auth", HttpMethod.Get);
    }

    public static LinkDto AllLink(string? href)
    {
        return new LinkDto(href, "all", HttpMethod.Get);
    }

    public static LinkDto CreateLink(string? href)
    {
        return new LinkDto(href, "create", HttpMethod.Post);
    }

    public static LinkDto DeleteLink(string? href)
    {
        return new LinkDto(href, "delete", HttpMethod.Delete);
    }

    public static LinkDto CurrentPage(string? href)
    {
        return new LinkDto(href, "currentPage", HttpMethod.Get);
    }

    public static LinkDto NextPage(string? href)
    {
        return new LinkDto(href, "nextPage", HttpMethod.Get);
    }

    public static LinkDto PreviousPage(string? href)
    {
        return new LinkDto(href, "previousPage", HttpMethod.Get);
    }

    public static LinkDto UpdateLink(string? href)
    {
        return new LinkDto(href, "update", HttpMethod.Put);
    }
}