using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Connection
    {
        [JsonPropertyName("asn")]
        public int? Asn { get; set; }

        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        [JsonPropertyName("organization")]
        public string? Organization { get; set; }

        [JsonPropertyName("route")]
        public string? Route { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}