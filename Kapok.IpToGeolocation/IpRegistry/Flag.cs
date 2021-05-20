// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Flag
    {
        [JsonPropertyName("emoji")]
        public string? Emoji { get; set; }

        [JsonPropertyName("emoji_unicode")]
        public string? EmojiUnicode { get; set; }

        [JsonPropertyName("emojitwo")]
        public string? Emojitwo { get; set; }

        [JsonPropertyName("noto")]
        public string? Noto { get; set; }

        [JsonPropertyName("twemoji")]
        public string? Twemoji { get; set; }

        [JsonPropertyName("wikimedia")]
        public string? Wikimedia { get; set; }
    }
}