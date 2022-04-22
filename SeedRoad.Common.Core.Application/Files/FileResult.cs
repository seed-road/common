namespace SeedRoad.Common.Core.Application.Files;

public record FileResult(Stream Content, string Extension) : IDisposable
{
    public void Dispose()
    {
        Content.Dispose();
    }

    public FileView To(string fileName) => new(Content, fileName, Extension);
}