using Microsoft.Extensions.Logging;
using SeedRoad.Common.Infrastructure.File.Contracts;

namespace SeedRoad.Common.Infrastructure.File.Services;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly string _basePath;

    public FileService(ILogger<FileService> logger, string basePath)
    {
        _logger = logger;
        _basePath = basePath;
    }

    public async Task WriteFileAsync(string filename, string extension, Stream content)
    {
        var filePath = GetFilePath(filename, extension);
        await using var outputFileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await content.CopyToAsync(outputFileStream);
    }

    private string GetFilePath(string filename, string extension) =>
        Path.Combine(_basePath, filename + "." + extension);


    public Stream LoadFile(string filename, string extension)
    {
        var filePath = GetFilePath(filename, extension);
        return new FileStream(filePath, FileMode.Open, FileAccess.Read);
    }

    public void DeleteFile(string filename, string extension)
    {
        var filePath = GetFilePath(filename, extension);
        if (!System.IO.File.Exists(filePath))
        {
            return;
        }

        System.IO.File.Delete(filePath);
    }
}

public class FileService<T> : FileService, IFileService<T>
{
    public FileService(ILogger<FileService<T>> logger, string basePath) : base(logger, basePath)
    {
    }
}