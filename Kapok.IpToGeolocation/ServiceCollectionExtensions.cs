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
                    if (context.TryGetValue("request", out var requestObject) && requestObject is HttpRequestMessage request
                    && context.TryGetValue("handler", out var handlerObject) && handlerObject is GeolocationRequestHandler handler)
                    {
                        var source = handler.SetRequestMessageUri(request, retryCount);
                        context["source"] = source;
                    }
                }));

        public static IHttpClientBuilder AddGeolocationService(this IServiceCollection services, int retryCount)
            => services.AddHttpClient<GeolocationService>()
                .AddGeolocationServicePolicyHandler(retryCount);
    }
}