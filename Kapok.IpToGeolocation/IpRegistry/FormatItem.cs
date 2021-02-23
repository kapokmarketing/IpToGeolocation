using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class FormatItem
    {
        [JsonPropertyName("prefix")]
        public string? Prefix { get; set; }

        [JsonPropertyName("suffix")]
        public string? Suffix { get; set; }
    }
}