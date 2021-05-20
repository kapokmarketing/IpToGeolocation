// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class TimeZone
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("abbreviation")]
        public string? Abbreviation { get; set; }

        [JsonPropertyName("current_time")]
        public DateTime? CurrentTime { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("offset")]
        public int? Offset { get; set; }

        [JsonPropertyName("in_daylight_saving")]
        public bool? InDaylightSaving { get; set; }
    }
}