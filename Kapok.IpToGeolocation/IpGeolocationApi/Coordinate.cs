using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpGeolocationApi
{
    public class Coordinate
    {
        [JsonPropertyName("lat")]
        public double? Lat { get; set; }

        [JsonPropertyName("lng")]
        public double? Lng { get; set; }
    }


}
