using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Country
    {
        [JsonPropertyName("area")]
        public int? Area { get; set; }

        [JsonPropertyName("borders")]
        public List<string>? Borders { get; set; }

        [JsonPropertyName("calling_code")]
        public string? CallingCode { get; set; }

        [JsonPropertyName("capital")]
        public string? Capital { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("population")]
        public int? Population { get; set; }

        [JsonPropertyName("population_density")]
        public double? PopulationDensity { get; set; }

        [JsonPropertyName("flag")]
        public Flag? Flag { get; set; }

        [JsonPropertyName("languages")]
        public List<Language>? Languages { get; set; }

        [JsonPropertyName("tld")]
        public string? Tld { get; set; }
    }
}