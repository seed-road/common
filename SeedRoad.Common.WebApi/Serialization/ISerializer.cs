namespace SeedRoad.Common.WebApi.Serialization;

public interface ISerializer
{
    public string Serialize<TData>(TData serializableData);
}