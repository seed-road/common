using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SeedRoad.Common.Core.Application.Files;
using SeedRoad.Common.Presentation.WebApi.Http;

namespace SeedRoad.Common.Presentation.WebApi;

[ApiController]
[Route("api/v{version:apiVersion}/" + TemplateControllerName)]
public abstract class ApiControllerBase : ControllerBase
{
    protected const string TemplateActionName = "[action]";
    protected const string TemplateControllerName = "[controller]";

    protected Guid? UserId
    {
        get
        {
            var subClaim = User.FindFirstValue("sub") ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(subClaim)) return null;
            return Guid.Parse(subClaim);
        }
    }

    protected FileStreamResult GetFileStreamResult(FileView fileView)
    {
        fileView.Content.Seek(0, SeekOrigin.Begin);
        return new FileStreamResult(fileView.Content, MimeHelper.GetMimeType(fileView.Extension))
        {
            FileDownloadName = $"{fileView.Name}{fileView.Extension}"
        };
    }
}