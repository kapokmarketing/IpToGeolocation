// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.AbstractApi
{
    public class Result : IGeolocationSourceResult
    {
        public Provider Source => Provider.AbstractApi;
        public bool Success => true;

        [JsonPropertyName("ip_address")]
        public string? IpAddress { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("city_geoname_id")]
        public int? CityGeonameId { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("region_iso_code")]
        public string? RegionIsoCode { get; set; }

        [JsonPropertyName("region_geoname_id")]
        public int? RegionGeonameId { get; set; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("country_geoname_id")]
        public int? CountryGeonameId { get; set; }

        [JsonPropertyName("country_is_eu")]
        public bool? CountryIsEu { get; set; }

        [JsonPropertyName("continent")]
        public string? Continent { get; set; }

        [JsonPropertyName("continent_code")]
        public string? ContinentCode { get; set; }

        [JsonPropertyName("continent_geoname_id")]
        public int? ContinentGeonameId { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("security")]
        public Security? Security { get; set; }

        [JsonPropertyName("timezone")]
        public Timezone? Timezone { get; set; }

        [JsonPropertyName("flag")]
        public Flag? Flag { get; set; }

        [JsonPropertyName("currency")]
        public Currency? Currency { get; set; }

        [JsonPropertyName("connection")]
        public Connection? Connection { get; set; }

        int? IGeolocationDto.GeonameId => CityGeonameId;
        string? IGeolocationDto.ContinentName => Continent;
        string? IGeolocationDto.CountryName => Country;
        string? IGeolocationDto.TimeZoneCode => Timezone?.Abbreviation;
        string? IGeolocationDto.RegionCode => RegionIsoCode;
        string? IGeolocationDto.RegionName => Region;
    }
}
