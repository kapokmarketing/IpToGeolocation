﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.AbstractApi
{
    public class Flag
    {
        [JsonPropertyName("emoji")]
        public string? Emoji { get; set; }

        [JsonPropertyName("unicode")]
        public string? Unicode { get; set; }

        [JsonPropertyName("png")]
        public string? Png { get; set; }

        [JsonPropertyName("svg")]
        public string? Svg { get; set; }
    }
}
