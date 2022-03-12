using Microsoft.AspNetCore.Mvc;

namespace SeedRoad.Common.WebApi;

[ApiController]
[Route("api/v{version:apiVersion}/" + TemplateControllerName)]
public abstract class ApiControllerBase : ControllerBase
{
    protected const string TemplateActionName = "[action]";
    protected const string TemplateControllerName = "[controller]";
}