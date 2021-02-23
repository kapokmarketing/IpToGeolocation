using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.AbstractApi
{
    public class Timezone
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("abbreviation")]
        public string? Abbreviation { get; set; }

        [JsonPropertyName("gmt_offset")]
        public int? GmtOffset { get; set; }

        [JsonPropertyName("current_time")]
        public string? CurrentTime { get; set; }

        [JsonPropertyName("is_dst")]
        public bool? IsDst { get; set; }
    }
}
