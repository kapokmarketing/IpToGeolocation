// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpGeolocationApi
{
    public class Geo
    {
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("latitude_dec")]
        public string? LatitudeDec { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("longitude_dec")]
        public string? LongitudeDec { get; set; }

        [JsonPropertyName("max_latitude")]
        public double? MaxLatitude { get; set; }

        [JsonPropertyName("max_longitude")]
        public double? MaxLongitude { get; set; }

        [JsonPropertyName("min_latitude")]
        public double? MinLatitude { get; set; }

        [JsonPropertyName("min_longitude")]
        public double? MinLongitude { get; set; }

        [JsonPropertyName("bounds")]
        public Bounds? Bounds { get; set; }
    }


}
