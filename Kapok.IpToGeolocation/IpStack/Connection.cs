using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpStack
{
    public class Connection
    {
        [JsonPropertyName("asn")]
        public int? Asn { get; set; }

        [JsonPropertyName("isp")]
        public string? Isp { get; set; }
    }
}
