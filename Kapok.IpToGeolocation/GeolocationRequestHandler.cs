using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationRequestHandler
    {
        private readonly string _ipAddress;
        private readonly GeolocationProviderConfiguration[] _hosts;

        public GeolocationRequestHandler(GeolocationProviderConfiguration[] hosts, string ipAddress)
            => (_hosts, _ipAddress) = (hosts, ipAddress);

        public Provider SetRequestMessageUri(HttpRequestMessage requestMessage, int retryCount = 0)
        {
            var host = _hosts[retryCount];
            var requestUri = host.UrlPatternWithPrivateKey?.Replace("{IpAddress}", _ipAddress);

            if (requestUri == null)
                throw new NotImplementedException("Geolocation handler cannot generate an appropriate uri for this request.");

            requestMessage.RequestUri = new Uri(requestUri);

            return host.Name;
        }
    }
}