namespace SeedRoad.Common.Infrastructure.File.Contracts;

public interface IFileService
{
    public Task WriteFileAsync(string filename, string extension, Stream content);
}

public interface IFileService<T> : IFileService
{
}

