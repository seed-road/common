using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SeedRoad.Common.Presentation.WebApi.Serialization;

public class JsonSerializer : ISerializer
{
    public string Serialize<TData>(TData serializableData)
    {
        return JsonConvert.SerializeObject(serializableData,
            new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });
    }
}