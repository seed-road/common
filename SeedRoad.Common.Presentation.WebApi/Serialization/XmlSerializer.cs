namespace SeedRoad.Common.Presentation.WebApi.Serialization;

public class XmlSerializer : ISerializer
{
    public string Serialize<TData>(TData serializableData)
    {
        var serializer = new global::System.Xml.Serialization.XmlSerializer(typeof(TData));
        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, serializableData);
        return stringWriter.ToString();
    }
}