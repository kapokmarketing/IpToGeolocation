// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpStack
{
    public class Location
    {
        [JsonPropertyName("geoname_id")]
        public int? GeonameId { get; set; }

        [JsonPropertyName("capital")]
        public string? Capital { get; set; }

        [JsonPropertyName("languages")]
        public List<Language>? Languages { get; set; }

        [JsonPropertyName("country_flag")]
        public string? CountryFlag { get; set; }

        [JsonPropertyName("country_flag_emoji")]
        public string? CountryFlagEmoji { get; set; }

        [JsonPropertyName("country_flag_emoji_unicode")]
        public string? CountryFlagEmojiUnicode { get; set; }

        [JsonPropertyName("calling_code")]
        public string? CallingCode { get; set; }

        [JsonPropertyName("is_eu")]
        public bool? IsEu { get; set; }
    }
}
