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
            var handler = GetTestHandlerEmpty();

            Assert.ThrowsException<GeolocationException>(() =>
            {
                _ = handler.SetRequestMessageUri(new HttpRequestMessage(), 0);
            });
        }

        [TestMethod]
        public void Handler_ShouldReturnProvider()
        {
            var expectedValue = Provider.AbstractApi;
            var handler = GetTestHandler(expectedValue);

            var provider = handler.SetRequestMessageUri(new HttpRequestMessage(), 0);

            Assert.AreEqual(expectedValue, provider);
        }

        [TestMethod]
        public void Handler_ShouldSetRequestUri()
        {
            var expectedValue = Provider.AbstractApi;
            var handler = GetTestHandler(expectedValue);
            var request = new HttpRequestMessage();

            // Act
            var provider = handler.SetRequestMessageUri(request, 0);

            Assert.AreEqual(TestUrl, request.RequestUri.ToString());
        }
    }
}
