using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{

    public class Result : IGeolocationSourceResult, IGeolocationDto
    {
        public Provider Source => Provider.IpRegistry;

        [JsonPropertyName("ip")]
        public string? Ip { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("hostname")]
        public object? Hostname { get; set; }

        [JsonPropertyName("carrier")]
        public Carrier? Carrier { get; set; }

        [JsonPropertyName("connection")]
        public Connection? Connection { get; set; }

        [JsonPropertyName("currency")]
        public Currency? Currency { get; set; }

        [JsonPropertyName("location")]
        public Location? Location { get; set; }

        [JsonPropertyName("security")]
        public Security? Security { get; set; }

        [JsonPropertyName("time_zone")]
        public TimeZone? TimeZone { get; set; }

        [JsonPropertyName("user_agent")]
        public UserAgent? UserAgent { get; set; }

        int? IGeolocationDto.GeonameId => null;

        string? IGeolocationDto.City => Location?.City;

        string? IGeolocationDto.ContinentCode => Location?.Continent?.Code;

        string? IGeolocationDto.ContinentName => Location?.Continent?.Name;

        string? IGeolocationDto.CountryCode => Location?.Country?.Code;

        string? IGeolocationDto.CountryName => Location?.Country?.Name;

        double? IGeolocationDto.Latitude => Location?.Latitude;

        double? IGeolocationDto.Longitude => Location?.Longitude;

        string? IGeolocationDto.TimeZoneCode => TimeZone?.Abbreviation;

        string? IGeolocationDto.RegionCode => Location?.Region?.Code;

        string? IGeolocationDto.RegionName => Location?.Region?.Name;

        string? IGeolocationDto.PostalCode => Location?.Postal;
    }
}