// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Security
    {
        [JsonPropertyName("is_bogon")]
        public bool? IsBogon { get; set; }

        [JsonPropertyName("is_cloud_provider")]
        public bool? IsCloudProvider { get; set; }

        [JsonPropertyName("is_tor")]
        public bool? IsTor { get; set; }

        [JsonPropertyName("is_tor_exit")]
        public bool? IsTorExit { get; set; }

        [JsonPropertyName("is_proxy")]
        public bool? IsProxy { get; set; }

        [JsonPropertyName("is_anonymous")]
        public bool? IsAnonymous { get; set; }

        [JsonPropertyName("is_abuser")]
        public bool? IsAbuser { get; set; }

        [JsonPropertyName("is_attacker")]
        public bool? IsAttacker { get; set; }

        [JsonPropertyName("is_threat")]
        public bool? IsThreat { get; set; }
    }
}