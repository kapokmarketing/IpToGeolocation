using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpStack
{
    public class Language
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("native")]
        public string? Native { get; set; }
    }
}
