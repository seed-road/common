using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SeedRoad.Common.WebApi.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddCommonNewtonsoftJson(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
    }
}