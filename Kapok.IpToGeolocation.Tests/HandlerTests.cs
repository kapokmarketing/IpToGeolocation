// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;

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
        private GeolocationRequestHandler GetTestHandler(Provider[] providers)
            => new GeolocationRequestHandler(providers.Select(provider => GetTestProvider(provider)).ToArray(), TestIpAddress);
        private GeolocationRequestHandler GetTestHandlerEmpty()
            => new GeolocationRequestHandler(Array.Empty<GeolocationProviderConfiguration>(), TestIpAddress);

        [TestMethod]
        public void Handler_ShouldReturnProvider()
        {
            // Arrange
            var expectedValue = Provider.AbstractApi;
            var handler = GetTestHandler(expectedValue);

            // Act
            var (provider, _) = handler.SetRequestMessageUri(new HttpRequestMessage(), providerIndex: 0, Array.Empty<Provider>());

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
            _ = handler.SetRequestMessageUri(request, 0, Array.Empty<Provider>());

            // Assert
            Assert.AreEqual(TestUrl, request.RequestUri.ToString());
        }

        [DataRow(0)]
        [DataRow(1)]
        [DataTestMethod]
        public void Handler_ShouldIncrementProviderIndex(int providerIndex)
        {
            // Arrange
            var providers = new Provider[] { Provider.AbstractApi, Provider.IpGeolocationApi };
            var expectedNextProviderIndex = (providerIndex + 1) % providers.Length;
            var handler = GetTestHandler(providers);
            var request = new HttpRequestMessage();

            // Act
            var (_, nextIndex) = handler.SetRequestMessageUri(request, providerIndex, Array.Empty<Provider>());

            // Assert
            Assert.AreEqual(expectedNextProviderIndex, nextIndex % providers.Length);
        }

        [TestMethod]
        public void Handler_WhenEmpty_ShouldThrowGeolocationException()
        {
            // Arrange
            var handler = GetTestHandlerEmpty();

            // Act
            Action action = () =>
            {
                _ = handler.SetRequestMessageUri(new HttpRequestMessage(), providerIndex: 0, Array.Empty<Provider>());
            };

            // Assert
            Assert.ThrowsException<GeolocationException>(action);
        }

        [DataRow(0, Provider.AbstractApi)]
        [DataRow(0, Provider.IpGeolocationApi)]
        [DataRow(1, Provider.AbstractApi)]
        [DataRow(1, Provider.IpGeolocationApi)]
        [DataTestMethod]
        public void Handler_WhenProviderIgnored_ShouldNotReturnIgnoredProvider(int providerIndex, Provider notExpected)
        {
            // Arrange
            var providers = new Provider[] { Provider.AbstractApi, Provider.IpGeolocationApi };
            var handler = GetTestHandler(providers);
            var request = new HttpRequestMessage();

            // Act
            var (provider, nextIndex) = handler.SetRequestMessageUri(request, providerIndex, new Provider[] { notExpected });

            // Assert
            Assert.IsNotNull(provider);
            Assert.AreNotEqual(notExpected, provider);
        }

        [DataRow(0, new Provider[] { Provider.AbstractApi, Provider.IpGeolocationApi })]
        [DataRow(0, new Provider[] { Provider.AbstractApi })]
        [DataTestMethod]
        public void Handler_WhenAllIgnored_ShouldThrowGeolocationException(int providerIndex, Provider[] providers)
        {
            // Arrange
            var handler = GetTestHandler(providers);
            var request = new HttpRequestMessage();

            // Act
            Action action = () =>
            {
                _ = handler.SetRequestMessageUri(request, providerIndex: 0, providers);
            };

            // Assert
            Assert.ThrowsException<GeolocationException>(action);
        }
    }
}
