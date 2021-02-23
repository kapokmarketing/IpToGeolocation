using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpGeolocationApi
{
    public class VatRates
    {
        [JsonPropertyName("standard")]
        public int? Standard { get; set; }

        [JsonPropertyName("reduced")]
        public List<int>? Reduced { get; set; }

        [JsonPropertyName("super_reduced")]
        public object? SuperReduced { get; set; }

        [JsonPropertyName("parking")]
        public object? Parking { get; set; }
    }


}
