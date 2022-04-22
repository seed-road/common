using Microsoft.Extensions.Logging;
using SeedRoad.Common.Core.Application.Files;

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

    public async Task WriteFileAndRemoveFilesStartingByTheSameNameAsync(string filename, string extension,
        Stream content)
    {
        DeleteFile(filename);
        await WriteFileAsync(filename, extension, content);
    }


    private string GetFilePath(string filename, string extension) =>
        Path.Combine(_basePath, filename + extension);

    private string? FindFileWithoutExtension(string filename)
    {
        return Directory.GetFiles(_basePath).FirstOrDefault(f => Path.GetFileName(f).StartsWith(filename));
    }

    public async Task<FileResult?> LoadFileAsync(string filename, string extension)
    {
        var filePath = GetFilePath(filename, extension);
        if (!global::System.IO.File.Exists(filePath)) return null;

        return await GetFileResultFromFile(filePath);
    }

    private static async Task<FileResult?> GetFileResultFromFile(string filePath)
    {
        var memStream = new MemoryStream();
        await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        await fileStream.CopyToAsync(memStream);
        return new FileResult(
            memStream,
            Path.GetExtension(filePath));
    }

    public async Task<FileResult?> LoadFileAsync(string filenameWithoutExtension)
    {
        var filePath = FindFileWithoutExtension(filenameWithoutExtension);
        if (filePath is null || !global::System.IO.File.Exists(filePath))
        {
            return null;
        }
        return await GetFileResultFromFile(filePath);
    }


    public void DeleteFile(string filename, string extension)
    {
        var filePath = GetFilePath(filename, extension);
        if (!global::System.IO.File.Exists(filePath))
        {
            return;
        }

        global::System.IO.File.Delete(filePath);
    }

    public void DeleteFile(string filenameWithoutExtension)
    {
        var filePath = FindFileWithoutExtension(filenameWithoutExtension);
        if (filePath is null || !global::System.IO.File.Exists(filePath))
        {
            return;
        }

        global::System.IO.File.Delete(filePath);
    }
}

public class FileService<T> : FileService, IFileService<T>
{
    public FileService(ILogger<FileService<T>> logger, string basePath) : base(logger, basePath)
    {
    }
}