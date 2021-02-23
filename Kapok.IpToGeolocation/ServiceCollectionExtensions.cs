using Kapok.IpToGeolocation;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddGeolocationServicePolicyHandler(this IHttpClientBuilder builder, int retryCount)
            => builder.AddPolicyHandler(Policy.HandleInner<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(retryCount, (ex, retryCount, context) =>
                {
                    if (context.TryGetValue(GeolocationService.CONTEXT_KEY_REQUEST, out var temp) && temp is HttpRequestMessage request
                    && context.TryGetValue(GeolocationService.CONTEXT_KEY_HANDLER, out temp) && temp is IGeolocationRequestHandler handler)
                    {
                        var provider = handler.SetRequestMessageUri(request, retryCount);
                        context[GeolocationService.CONTEXT_KEY_PROVIDER] = provider;
                        context[GeolocationService.CONTEXT_KEY_RETRY_COUNT] = retryCount;
                    }
                }));

        public static IHttpClientBuilder AddGeolocationService(this IServiceCollection services, int retryCount)
            => services.AddHttpClient<GeolocationService>()
                .AddGeolocationServicePolicyHandler(retryCount);
    }
}