namespace SeedRoad.Common.Core.Application.Files;

public interface IFileService
{
    Task WriteFileAsync(string filename, string extension, Stream content);

    Task WriteFileAndRemoveFilesStartingByTheSameNameAsync(string filename, string extension,
        Stream content);

    Task<FileResult?> LoadFileAsync(string filename, string extension);
    Task<FileResult?> LoadFileAsync(string filenameWithoutExtension);
    void DeleteFile(string filename, string extension);
    void DeleteFile(string filenameWithoutExtension);
}

public interface IFileService<T> : IFileService
{
}

