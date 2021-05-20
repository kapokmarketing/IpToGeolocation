// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kapok.IpToGeolocation.Tests
{
    [TestClass]
    public class HandlerTests : BaseTests
    {
        private string TestIpAddress => "8.8.8.8";
        private string TestPrivateKey => "test";
        private string TestUrlPattern => "https://localhost/?ip={IpAddress}&key={Key}";
        private string TestUrl => $"https://localhost/?ip={TestIpAddress}&key={TestPrivateKey}";

        private GeolocationProviderConfiguration GetTestProvider(Provider provider)
            => new GeolocationProviderConfiguration { Name = provider, Disabled = false, PrivateKey = TestPrivateKey, UrlPattern = TestUrlPattern };
        private GeolocationProviderConfiguration[] GetTestProviders(Provider provider)
            => new [] { GetTestProvider(provider) };
        private GeolocationRequestHandler GetTestHandler(Provider provider)
            => new GeolocationRequestHandler(GetTestProviders(provider), TestIpAddress);
        private GeolocationRequestHandler GetTestHandlerEmpty()
            => new GeolocationRequestHandler(Array.Empty<GeolocationProviderConfiguration>(), TestIpAddress);

        [TestMethod]
        public void Handler_WhenEmpty_ShouldThrowGeolocationException()
        {
            // Arrange
            var handler = GetTestHandlerEmpty();

            // Act
            var action = new Action(() =>
            {
                _ = handler.SetRequestMessageUri(new HttpRequestMessage(), 0);
            });

            // Assert
            Assert.ThrowsException<GeolocationException>(action);
        }

        [TestMethod]
        public void Handler_ShouldReturnProvider()
        {
            // Arrange
            var expectedValue = Provider.AbstractApi;
            var handler = GetTestHandler(expectedValue);

            // Act
            var provider = handler.SetRequestMessageUri(new HttpRequestMessage(), 0);

            // Assert
            Assert.AreEqual(expectedValue, provider);
        }

        [TestMethod]
        public void Handler_ShouldSetRequestUri()
        {
            // Arrange
            var expectedValue = Provider.AbstractApi;
            var handler = GetTestHandler(expectedValue);
            var request = new HttpRequestMessage();

            // Act
            _ = handler.SetRequestMessageUri(request, 0);

            // Assert
            Assert.AreEqual(TestUrl, request.RequestUri.ToString());
        }
    }
}
