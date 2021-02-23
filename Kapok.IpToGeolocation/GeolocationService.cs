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
        public const string CONTEXT_KEY_PROVIDER = "provider";
        public const string CONTEXT_KEY_HANDLER = "handler";
        public const string CONTEXT_KEY_REQUEST = "request";
        public const string CONTEXT_KEY_RETRY_COUNT = "retryCount";
        public const string CONFIGURATION_SECTION_NAME = "IpToGeolocation";

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
            => _configuration.Providers.Values.Where(p => !p.Disabled).ToArray();

        private GeolocationProviderConfiguration[] GetProviders(IEnumerable<Provider> providers)
            => _configuration.Providers.Values.Where(p => !p.Disabled && providers.Contains(p.Name)).ToArray();

        private (HttpRequestMessage, Context) GetHttpRequestMessage(string ipAddress, IEnumerable<Provider>? validProviders)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get
            };
            var providers = validProviders != null ? GetProviders(validProviders) : GetProviders();
            var handler = new GeolocationRequestHandler(providers, ipAddress);
            var provider = handler.SetRequestMessageUri(request);
            var context = new Context($"GeolocationFor{ipAddress}", new Dictionary<string, object>()
            {
                { CONTEXT_KEY_HANDLER, handler },
                { CONTEXT_KEY_REQUEST, request },
                { CONTEXT_KEY_PROVIDER, provider }
            });

            request.SetPolicyExecutionContext(context);

            return (request, context);
        }

        private Provider GetProvider(Context context)
        {
            if (context.TryGetValue(CONTEXT_KEY_PROVIDER, out var temp) && temp is Provider provider)
            {
                return provider;
            }

            return Provider.Unknown;
        }

        private int GetRetryCount(Context context)
        {
            if (context.TryGetValue(CONTEXT_KEY_RETRY_COUNT, out var temp) && temp is int retryCount)
            {
                return retryCount;
            }

            return 0;
        }

        public async Task<GeolocationResult> GetAsync(string ipAddress, IEnumerable<Provider>? validProviders = null, CancellationToken cancellationToken = default)
        {
            var (request, context) = GetHttpRequestMessage(ipAddress, validProviders);
            var response = await _httpClient.SendAsync(request, cancellationToken);
            var retryCount = GetRetryCount(context);

            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogWarning("Final retry attempt ({RetryCount}) for geolocation of {IpAddress} failed with status code {StatusCode} for {OperationKey}. Content will not be processed.",
                    retryCount, ipAddress, response.StatusCode, context?.OperationKey);
                return new GeolocationResult(ipAddress, retryCount);
            }

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var source = GetProvider(context);
                if (source == Provider.Unknown)
                {
                    _logger?.LogWarning("Final retry attempt ({RetryCount}) for geolocation of {IpAddress} context is missing the source for {OperationKey}. Deserialization cannot occur.",
                        retryCount, ipAddress, context?.OperationKey);
                    return new GeolocationResult(ipAddress, retryCount);
                }

                var dto = await GeolocationSerializer.DeserializeAsync(source, responseStream, cancellationToken);
                return new GeolocationResult(ipAddress, dto, source, retryCount);
            }
        }
    }
}