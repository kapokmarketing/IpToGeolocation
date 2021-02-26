using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpGeolocationApi
{
    public class Result : IGeolocationSourceResult
    {
        public Provider Source => Provider.IpGeolocationApi;
        public bool Success => true;

        [JsonPropertyName("continent")]
        public string? Continent { get; set; }

        [JsonPropertyName("address_format")]
        public string? AddressFormat { get; set; }

        [JsonPropertyName("alpha2")]
        public string? Alpha2 { get; set; }

        [JsonPropertyName("alpha3")]
        public string? Alpha3 { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("international_prefix")]
        public string? InternationalPrefix { get; set; }

        [JsonPropertyName("ioc")]
        public string? Ioc { get; set; }

        [JsonPropertyName("gec")]
        public string? Gec { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("national_destination_code_lengths")]
        public List<int>? NationalDestinationCodeLengths { get; set; }

        [JsonPropertyName("national_number_lengths")]
        public List<int>? NationalNumberLengths { get; set; }

        [JsonPropertyName("national_prefix")]
        public string? NationalPrefix { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("subregion")]
        public string? Subregion { get; set; }

        [JsonPropertyName("world_region")]
        public string? WorldRegion { get; set; }

        [JsonPropertyName("un_locode")]
        public string? UnLocode { get; set; }

        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }

        [JsonPropertyName("eu_member")]
        public bool? EuMember { get; set; }

        [JsonPropertyName("eea_member")]
        public bool? EeaMember { get; set; }

        [JsonPropertyName("vat_rates")]
        public VatRates? VatRates { get; set; }

        [JsonPropertyName("postal_code")]
        public bool? PostalCode { get; set; }

        [JsonPropertyName("unofficial_names")]
        public List<string>? UnofficialNames { get; set; }

        [JsonPropertyName("languages_official")]
        public List<string>? LanguagesOfficial { get; set; }

        [JsonPropertyName("languages_spoken")]
        public List<string>? LanguagesSpoken { get; set; }

        [JsonPropertyName("geo")]
        public Geo? Geo { get; set; }

        [JsonPropertyName("currency_code")]
        public string? CurrencyCode { get; set; }

        [JsonPropertyName("start_of_week")]
        public string? StartOfWeek { get; set; }

        int? IGeolocationDto.GeonameId => null;

        string? IGeolocationDto.City => null;

        string? IGeolocationDto.ContinentCode => null;
        string? IGeolocationDto.ContinentName => this.Continent;
        string? IGeolocationDto.CountryCode => this.Alpha2;
        string? IGeolocationDto.CountryName => this.Name;
        double? IGeolocationDto.Latitude => this.Geo?.Latitude;
        double? IGeolocationDto.Longitude => this.Geo?.Longitude;
        string? IGeolocationDto.TimeZoneCode => null;
        string? IGeolocationDto.RegionCode => null;
        string? IGeolocationDto.RegionName => null;
        string? IGeolocationDto.PostalCode => null;
    }
}