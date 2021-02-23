using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Format
    {
        [JsonPropertyName("negative")]
        public FormatItem? Negative { get; set; }

        [JsonPropertyName("positive")]
        public FormatItem? Positive { get; set; }
    }
}