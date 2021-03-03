using System.Text.Json;

namespace HttpClients
{
    public static class StreamSerialisation
    {
        public static JsonSerializerOptions CamelCaseSerialisationOption = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    }
}
