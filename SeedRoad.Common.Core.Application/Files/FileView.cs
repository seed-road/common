namespace SeedRoad.Common.Core.Application.Files;

public record FileView(Stream Content, string Name, string Extension) : IDisposable
{
    public void Dispose()
    {
        Content.Dispose();
    }
}