using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpStack
{
    public class Error
    {
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("type")]
        public ErrorType? Type { get; set; }

        [JsonPropertyName("info")]
        public string? Info { get; set; }
    }
}
