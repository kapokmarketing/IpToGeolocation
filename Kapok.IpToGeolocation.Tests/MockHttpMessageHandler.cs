// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly;

namespace Kapok.IpToGeolocation.Tests
{
    public class MockHttpMessageHandler : DelegatingHandler
    {
        private readonly Provider _provider;
        private readonly HttpStatusCode? _firstStatusCode;
        private readonly HttpStatusCode _statusCode;

        public MockHttpMessageHandler(Provider provider, HttpStatusCode? firstStatusCode, HttpStatusCode statusCode)
            => (_provider, _firstStatusCode, _statusCode) = (provider, firstStatusCode, statusCode);

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = request.GetPolicyExecutionContext();
            var retryCount = context.GetRetryCount();
            var provider = context.GetProvider();

            var content = TestData.GetJson(provider);
            var response = new HttpResponseMessage
            {
                RequestMessage = request,
                StatusCode = retryCount > 0 ? _statusCode : (_firstStatusCode ?? _statusCode),
                Content = new StringContent(content),
            };

            return Task.FromResult(response);
        }
    }
}
