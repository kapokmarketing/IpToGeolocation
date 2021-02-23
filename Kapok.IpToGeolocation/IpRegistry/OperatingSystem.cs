using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class OperatingSystem
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }
    }
}