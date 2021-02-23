using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;
using System.Linq;

namespace Kapok.IpToGeolocation
{
    public class GeolocationService
    {
        private const string CONFIGURATION_SECTION_NAME = "IpToGeolocation";

        private readonly ILogger<GeolocationService>? _logger;
        private readonly IGeolocationConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GeolocationService(IConfiguration configuration, HttpClient httpClient, ILogger<GeolocationService>? logger)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration.GetSection(CONFIGURATION_SECTION_NAME).Get<GeolocationConfiguration>();
        }

        private GeolocationProviderConfiguration[] GetProviders()
            => _configuration.Providers.Where(p => !p.Disabled).ToArray();

        private (HttpRequestMessage, Context) GetHttpRequestMessage(string ipAddress)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get
            };
            var handler = new GeolocationRequestHandler(GetProviders(), ipAddress);
            var source = handler.SetRequestMessageUri(request);
            var context = new Context($"GeolocationFor{ipAddress}", new Dictionary<string, object>()
            {
                { "handler", handler },
                { "request", request },
                { "source", source }
            });

            request.SetPolicyExecutionContext(context);

            return (request, context);
        }

        private Provider GetProvider(Context context)
        {
            if (context.TryGetValue("source", out var sourceObject) && sourceObject is Provider source)
            {
                return source;
            }

            return Provider.Unknown;
        }

        public async Task<GeolocationResult> GetAsync(string ipAddress, CancellationToken cancellationToken)
        {
            var (request, context) = GetHttpRequestMessage(ipAddress);
            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogWarning("Final retry attempt for geolocation of {IpAddress} failed with status code {StatusCode} for {OperationKey}.",
                    ipAddress, response.StatusCode, context.OperationKey);
                return new GeolocationResult(ipAddress);
            }

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var source = GetProvider(context);
                if (source == Provider.Unknown)
                {
                    _logger?.LogWarning("Final retry attempt for geolocation of {IpAddress} context is missing the source. Deserialization cannot occur.",
                        ipAddress, response.StatusCode, context.OperationKey);
                    return new GeolocationResult(ipAddress);
                }

                var dto = await GeolocationSerializer.DeserializeAsync(source, responseStream, cancellationToken);
                return new GeolocationResult(ipAddress, dto, source);
            }
        }
    }
}