using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationRequestHandler : IGeolocationRequestHandler
    {
        private readonly string _ipAddress;
        private readonly GeolocationProviderConfiguration[] _hosts;

        public GeolocationRequestHandler(GeolocationProviderConfiguration[] hosts, string ipAddress)
            => (_hosts, _ipAddress) = (hosts, ipAddress);

        /// <summary>
        /// Determine the provider for the current attempt to find a geolocation for <see cref="_ipAddress"/>, and
        /// use that provider to set the <see cref="HttpRequestMessage.RequestUri"/> on <paramref name="requestMessage"/>.
        /// </summary>
        /// <param name="requestMessage">The request to set the uri.</param>
        /// <param name="retryCount">The current retry count.</param>
        /// <returns>The selected <see cref="Provider"/>.</returns>
        /// <exception cref="GeolocationException">Thrown when <see cref="_hosts"/> is empty or contains an invalid <see cref="GeolocationProviderConfiguration"/>.</exception>
        /// <exception cref="UriFormatException">Thrown by <see cref="Uri"/> when the uri for the current provider is malformed.</exception>
        public Provider SetRequestMessageUri(HttpRequestMessage requestMessage, int retryCount = 0)
        {
            if (_hosts.Length == 0)
            {
                throw new GeolocationException("Geolocation handler has no valid hosts for this request.");
            }

            var index = retryCount > 0 ? retryCount % _hosts.Length : 0;
            var host = _hosts[index];
            var requestUri = host.UrlPatternWithPrivateKey?.Replace("{IpAddress}", _ipAddress);

            if (requestUri == null)
            {
                throw new GeolocationException("Geolocation handler could not generate an appropriate uri for this request.");
            }

            requestMessage.RequestUri = new Uri(requestUri);

            return host.Name;
        }
    }
}