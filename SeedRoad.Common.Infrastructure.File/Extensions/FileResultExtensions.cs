using SeedRoad.Common.Core.Application.Files;
using SeedRoad.Common.Infrastructure.File.Dtos;

namespace SeedRoad.Common.Infrastructure.File.Extensions;

public static class FileResultExtensions
{
    public static FileView ToFileView(this FileResult fileResult, string fileNameWithOutExtension) =>
        new(fileResult.Content, fileNameWithOutExtension, fileResult.Extension);
}