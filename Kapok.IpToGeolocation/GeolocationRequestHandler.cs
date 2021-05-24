// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationRequestHandler : IGeolocationRequestHandler
    {
        private readonly string _ipAddress;
        private readonly GeolocationProviderConfiguration[] _providerConfiguration;

        public GeolocationRequestHandler(GeolocationProviderConfiguration[] providerConfiguration, string ipAddress)
            => (_providerConfiguration, _ipAddress) = (providerConfiguration, ipAddress);

        public (Provider, int) SetRequestMessageUri(HttpRequestMessage requestMessage)
            => SetRequestMessageUri(requestMessage, providerIndex: 0, Array.Empty<Provider>());

        /// <summary>
        /// Determine the provider for the current attempt to find a geolocation for <see cref="_ipAddress"/>, and
        /// use that provider to set the <see cref="HttpRequestMessage.RequestUri"/> on <paramref name="requestMessage"/>.
        /// </summary>
        /// <param name="requestMessage">The request to set the uri.</param>
        /// <param name="providerIndex">The current host index.</param>
        /// <param name="providersToIgnore">Providers to ignore.</param>
        /// <returns>The selected <see cref="Provider"/> and the index to use next if we need another provider.</returns>
        /// <exception cref="GeolocationException">Thrown when <see cref="_providerConfiguration"/> is empty or contains an invalid <see cref="GeolocationProviderConfiguration"/>.</exception>
        /// <exception cref="UriFormatException">Thrown by <see cref="Uri"/> when the uri for the current provider is malformed.</exception>
        public (Provider, int) SetRequestMessageUri(HttpRequestMessage requestMessage, int providerIndex, Provider[] providersToIgnore)
        {
            if (_providerConfiguration.Length == 0)
            {
                throw new GeolocationException("Geolocation handler has no valid hosts for this request.");
            }

            providerIndex = providerIndex > 0 ? providerIndex % _providerConfiguration.Length : 0;
            var host = _providerConfiguration[providerIndex];

            if (providersToIgnore.Contains(host.Name))
            {
                if (providersToIgnore.Length >= _providerConfiguration.Length)
                {
                    throw new GeolocationException("Geolocation handler has no remaining providers for this request.");
                }

                return SetRequestMessageUri(requestMessage, providerIndex + 1, providersToIgnore);
            }

            var requestUri = host.UrlPatternWithPrivateKey?.Replace("{IpAddress}", _ipAddress);

            if (requestUri == null)
            {
                throw new GeolocationException("Geolocation handler could not generate an appropriate uri for this request.");
            }

            requestMessage.RequestUri = new Uri(requestUri);

            return (host.Name, providerIndex + 1);
        }
    }
}
