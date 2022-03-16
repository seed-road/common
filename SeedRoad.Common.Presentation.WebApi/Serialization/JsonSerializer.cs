namespace SeedRoad.Common.Presentation.WebApi.Serialization;

public class JsonSerializer : ISerializer
{
    public string Serialize<TData>(TData serializableData)
    {
        return global::System.Text.Json.JsonSerializer.Serialize(serializableData);
    }
}