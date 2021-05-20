// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpGeolocationApi
{
    public class Bounds
    {
        [JsonPropertyName("northeast")]
        public Coordinate? Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public Coordinate? Southwest { get; set; }
    }


}
