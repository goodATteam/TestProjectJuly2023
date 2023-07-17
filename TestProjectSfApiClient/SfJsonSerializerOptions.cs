using System.Text.Json;
using System.Text.Json.Serialization;

namespace TestProjectSfApiClient
{
    public static class SfJsonSerializerOptions
    {
        public static JsonSerializerOptions Default { get; } = new()
        {
            Converters =
            {
                new JsonStringEnumConverter(),
            },
        };
    }
}
