namespace SeedRoad.Common.Infrastructure.File.Dtos;

public record FileResult(Stream Content, string Extension) : IDisposable
{
    public void Dispose()
    {
        Content.Dispose();
    }
}