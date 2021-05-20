// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

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
