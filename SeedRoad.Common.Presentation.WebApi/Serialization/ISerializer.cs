namespace SeedRoad.Common.Presentation.WebApi.Serialization;

public interface ISerializer
{
    public string Serialize<TData>(TData serializableData);
}