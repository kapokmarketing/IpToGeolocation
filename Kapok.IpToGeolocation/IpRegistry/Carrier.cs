// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Carrier
    {
        [JsonPropertyName("name")]
        public object? Name { get; set; }

        [JsonPropertyName("mcc")]
        public object? Mcc { get; set; }

        [JsonPropertyName("mnc")]
        public object? Mnc { get; set; }
    }
}