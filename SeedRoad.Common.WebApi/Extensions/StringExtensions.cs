namespace SeedRoad.Common.WebApi.Extensions;

public static class StringExtensions
{
    public static string ControllerName(this string controllerClassName)
    {
        var controllerTextIndex = controllerClassName.IndexOf("Controller", StringComparison.Ordinal);
        var startingName = controllerClassName[..controllerTextIndex];
        return startingName.ToLowerInvariant();
    }
}