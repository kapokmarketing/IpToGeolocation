// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationDto
    {
        /// <summary>
        /// See: https://www.geonames.org/
        /// </summary>
        int? GeonameId { get; }
        string? City { get; }
        string? ContinentCode { get; }
        string? ContinentName { get; }
        string? CountryCode { get; }
        string? CountryName { get; }
        double? Latitude { get; }
        double? Longitude { get; }
        string? TimeZoneCode { get; }
        string? RegionCode { get; }
        string? RegionName { get; }
        string? PostalCode { get; }
    }
}