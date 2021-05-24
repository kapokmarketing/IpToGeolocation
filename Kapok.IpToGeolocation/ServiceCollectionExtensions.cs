// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using Kapok.IpToGeolocation;
using Polly;
using Polly.Retry;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        // request.GetPolicyExecutionContext()
        public static IHttpClientBuilder AddGeolocationServicePolicyHandler(this IHttpClientBuilder builder, int retryCount)
            => builder.AddPolicyHandler(Policy.HandleInner<HttpRequestException>()
                .OrResult<HttpResponseMessage>((response) => {
                    var successStatusCode = response.IsSuccessStatusCode;
                    var noContent = response.StatusCode == HttpStatusCode.NoContent;

                    if (noContent)
                    {
                        var context = response.RequestMessage.GetPolicyExecutionContext();
                        context[ContextKey.ProvidersFailed] = context.GetFailedProviders().Concat(new Provider[] { context.GetProvider() });
                    }

                    return !successStatusCode || noContent;
                })
                .RetryAsync(retryCount, (ex, retryCount, context) =>
                {
                    var request = context.GetRequest();
                    var handler = context.GetHandler();
                    var providerIndex = context.GetProviderIndex();
                    var failedProviders = context.GetFailedProviders();

                    if (request != null && handler != null)
                    {
                        var (provider, nextProviderIndex) = handler.SetRequestMessageUri(request, providerIndex, failedProviders);
                        context[ContextKey.Provider] = provider;
                        context[ContextKey.RetryCount] = retryCount;
                        context[ContextKey.ProviderIndex] = nextProviderIndex;
                    }
                }));

        public static IHttpClientBuilder AddGeolocationService(this IServiceCollection services, int retryCount)
            => services.AddHttpClient<GeolocationService>()
                .AddGeolocationServicePolicyHandler(retryCount);
    }
}
