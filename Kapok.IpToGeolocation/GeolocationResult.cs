using System;
using System.Collections.Generic;
using System.Text;

namespace Kapok.IpToGeolocation
{
    public class GeolocationResult : IGeolocationResult
    {
        public string IpAddress { get; set; }
        public Provider Provider { get; set; }
        public IGeolocationDto? Location { get; set; }
        public GeolocationResult(string ipAddress)
            => (IpAddress, Location, Provider) = (ipAddress, null, Provider.Unknown);
        public GeolocationResult(string ipAddress, IGeolocationDto location, Provider provider = Provider.Unknown)
            => (IpAddress, Location, Provider) = (ipAddress, GeolocationDto.Create(location), provider);
    }
}