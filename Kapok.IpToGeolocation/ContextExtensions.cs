// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Net.Http;
using Kapok.IpToGeolocation;

namespace Polly
{
    public static class ContextExtensions
    {
        public static IGeolocationRequestHandler? GetHandler(this Context context)
            => context.TryGetValue(ContextKey.Handler, out var temp) && temp is IGeolocationRequestHandler value ? value : null;

        public static HttpRequestMessage? GetRequest(this Context context)
            => context.TryGetValue(ContextKey.Request, out var temp) && temp is HttpRequestMessage value ? value : null;

        public static Provider GetProvider(this Context context)
            => GetContextItem(context, ContextKey.Provider, defaultValue: Provider.Unknown);

        public static int GetRetryCount(this Context context)
            => GetContextItem(context, ContextKey.RetryCount, defaultValue: 0);

        public static int GetProviderIndex(this Context context)
            => GetContextItem(context, ContextKey.ProviderIndex, defaultValue: 0);

        public static Provider[] GetFailedProviders(this Context context)
            => GetContextItem(context, ContextKey.ProvidersFailed, defaultValue: Array.Empty<Provider>());

        private static T GetContextItem<T>(this Context context, string key, T defaultValue)
            => context.TryGetValue(key, out var temp) && temp is T value ? value : defaultValue;
    }
}
