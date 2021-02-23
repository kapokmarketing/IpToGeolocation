using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpGeolocationApi
{
    public class Bounds
    {
        [JsonPropertyName("northeast")]
        public Coordinate? Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public Coordinate? Southwest { get; set; }
    }


}
