// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationDto : IGeolocationDto
    {
        public static GeolocationDto Create(IGeolocationDto dto)
            => dto is GeolocationDto result ? result : new GeolocationDto(dto);

        public int? GeonameId { get; }
        public string? ContinentCode { get; }
        public string? ContinentName { get; }
        public string? CountryCode { get; }
        public string? CountryName { get; }
        public string? RegionCode { get; }
        public string? RegionName { get; }
        public string? City { get; }
        public string? PostalCode { get; }
        public double? Latitude { get; }
        public double? Longitude { get; }
        public string? TimeZoneCode { get; }

        private GeolocationDto(IGeolocationDto dto)
        {
            GeonameId = dto.GeonameId;
            City = dto.City;
            ContinentCode = dto.ContinentCode;
            ContinentName = dto.ContinentName;
            CountryCode = dto.CountryCode;
            CountryName = dto.CountryName;
            Latitude = dto.Latitude;
            Longitude = dto.Longitude;
            TimeZoneCode = dto.TimeZoneCode;
            RegionCode = dto.RegionCode;
            RegionName = dto.RegionName;
            PostalCode = dto.PostalCode;
        }

        public override string ToString()
            => $"{City}, {RegionCode} {PostalCode} ({Latitude}, {Longitude})";
    }
}