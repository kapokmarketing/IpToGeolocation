using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class UserAgent
    {
        [JsonPropertyName("header")]
        public string? Header { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("version_major")]
        public string? VersionMajor { get; set; }

        [JsonPropertyName("device")]
        public Device? Device { get; set; }

        [JsonPropertyName("engine")]
        public Engine? Engine { get; set; }

        [JsonPropertyName("os")]
        public OperatingSystem? OperatingSystem { get; set; }
    }
}