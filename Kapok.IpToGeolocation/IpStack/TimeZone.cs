using System;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpStack
{
    public class TimeZone
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("current_time")]
        public DateTime? CurrentTime { get; set; }

        [JsonPropertyName("gmt_offset")]
        public int? GmtOffset { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("is_daylight_saving")]
        public bool? IsDaylightSaving { get; set; }
    }
}
