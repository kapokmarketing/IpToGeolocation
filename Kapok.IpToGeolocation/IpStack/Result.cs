using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpStack
{
    public partial class Result : IGeolocationSourceResult
    {
        public Provider Source => Provider.IpStack;

        [JsonPropertyName("success")]
        public bool Success { get; set; } = true;

        [JsonPropertyName("error")]
        public Error? Error { get; set; }

        [JsonPropertyName("ip")]
        public string? Ip { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("continent_code")]
        public string? ContinentCode { get; set; }

        [JsonPropertyName("continent_name")]
        public string? ContinentName { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string? CountryName { get; set; }

        [JsonPropertyName("region_code")]
        public string? RegionCode { get; set; }

        [JsonPropertyName("region_name")]
        public string? RegionName { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("zip")]
        public string? Zip { get; set; }

        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("location")]
        public Location? Location { get; set; }

        [JsonPropertyName("time_zone")]
        public TimeZone? TimeZone { get; set; }

        [JsonPropertyName("currency")]
        public Currency? Currency { get; set; }

        [JsonPropertyName("connection")]
        public Connection? Connection { get; set; }

        int? IGeolocationDto.GeonameId => Location?.GeonameId;
        string? IGeolocationDto.TimeZoneCode => TimeZone?.Code;
        string? IGeolocationDto.PostalCode => Zip;
    }
}
