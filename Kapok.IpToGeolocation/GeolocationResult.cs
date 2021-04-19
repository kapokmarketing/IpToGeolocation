using System;
using System.Collections.Generic;
using System.Text;

namespace Kapok.IpToGeolocation
{
    public class GeolocationResult : IGeolocationResult
    {
        public string IpAddress { get; set; }
        public Provider Provider { get; set; }
        public int RetryCount { get; set; }
        public IGeolocationDto? Location { get; set; }
        public GeolocationResult(string ipAddress)
            : this(ipAddress, 0)
        { }
        public GeolocationResult(string ipAddress, int retryCount)
            => (IpAddress, Location, Provider, RetryCount) = (ipAddress, null, Provider.Unknown, retryCount);
        public GeolocationResult(string ipAddress, IGeolocationDto location, Provider provider, int retryCount)
            => (IpAddress, Location, Provider, RetryCount) = (ipAddress, GeolocationDto.Create(location), provider, retryCount);
    }
}