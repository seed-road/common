using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace SeedRoad.Common.Presentation.WebApi.Routing;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object? value)
    {
        // Slugify value
        return value == null
            ? string.Empty
            : Regex.Replace(value.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}