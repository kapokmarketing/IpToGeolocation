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
        public static readonly string CONTEXT_KEY_PROVIDER = "provider";
        public static readonly string CONTEXT_KEY_HANDLER = "handler";
        public static readonly string CONTEXT_KEY_REQUEST = "request";
        public static readonly string CONTEXT_KEY_RETRY_COUNT = "retryCount";
        public static readonly string CONFIGURATION_SECTION_NAME = "IpToGeolocation";

        private readonly ILogger<GeolocationService>? _logger;
        private readonly IGeolocationConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public GeolocationService(IConfiguration configuration, HttpClient httpClient, ILogger<GeolocationService>? logger)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _logger = logger;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration.GetSection(CONFIGURATION_SECTION_NAME).Get<GeolocationConfiguration>();
        }

        private GeolocationProviderConfiguration[] GetProviders()
            => _configuration.Providers.Values.Where(p => !p.Disabled).OrderBy(p => p.Priority).ToArray();

        private GeolocationProviderConfiguration[] GetProviders(IEnumerable<Provider> providers)
            => _configuration.Providers.Values.Where(p => !p.Disabled && providers.Contains(p.Name)).OrderBy(p => p.Priority).ToArray();

        /// <summary>
        /// Create the <see cref="HttpRequestMessage"/> for the first valid and available provider.
        /// </summary>
        /// <param name="ipAddress">The IP address to include in the request to the provider.</param>
        /// <param name="validProviders">The list of valid providers to check for in <see cref="IGeolocationConfiguration.Providers"/>. If null, all available providers are valid.</param>
        /// <returns>If one or more providers are available, the HttpRequestMessage object. Otherwise, null.</returns>
        private (HttpRequestMessage?, Context?) GetHttpRequestMessage(string ipAddress, IEnumerable<Provider>? validProviders)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };
            var providers = validProviders != null ? GetProviders(validProviders) : GetProviders();

            if (providers.Length == 0)
            {
                _logger?.LogDebug("No valid providers found to create an HttpRequestMessage for geolocation of {IpAddress}.",
                    ipAddress);
                return (null!, null!);
            }

            var handler = new GeolocationRequestHandler(providers, ipAddress);
            var provider = handler.SetRequestMessageUri(request);
            var context = new Context($"GeolocationFor{ipAddress}", new Dictionary<string, object>
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

        public Task<GeolocationResult> GetAsync(string ipAddress, CancellationToken cancellationToken)
            => GetAsync(ipAddress, validProviders: null, cancellationToken);

        public async Task<GeolocationResult> GetAsync(string ipAddress, IEnumerable<Provider>? validProviders, CancellationToken cancellationToken)
        {
            var (request, context) = GetHttpRequestMessage(ipAddress, validProviders);
            if (request == null || context == null)
            {
                _logger?.LogWarning("Geolocation of {IpAddress} failed. No attempts were made, because a valid HttpRequestMessage or Polly.Context could not be properly initialized.",
                    ipAddress);
                return new GeolocationResult(ipAddress);
            }

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

                if (dto == null)
                {
                    _logger?.LogWarning("Attempt ({RetryCount}/??) for geolocation of {IpAddress} failed because of an unexpected result from the provider. Any remaining retries will not occur. Result: {Result}",
                        retryCount, ipAddress, dto);
                    return new GeolocationResult(ipAddress, retryCount);
                }

                return new GeolocationResult(ipAddress, dto, source, retryCount);
            }
        }
    }
}
