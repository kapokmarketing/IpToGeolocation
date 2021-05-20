// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

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