using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Region
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}