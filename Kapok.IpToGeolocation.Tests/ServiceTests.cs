// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Kapok.IpToGeolocation.Tests
{
    [TestClass]
    [DeploymentItem("Data")]
    public class ServiceTests : BaseTests
    {

        public static IEnumerable<object[]> GetSourcesWithIpAddresses()
        {
            yield return new object[] { Provider.IpGeolocationApi, "91.213.103.0", true };
            yield return new object[] { Provider.AbstractApi, "166.171.248.255", false };
        }

        //[DynamicData(nameof(GetSourcesWithIpAddresses), DynamicDataSourceType.Method)]
        //[DataTestMethod]
        //public async Task Service_WhenOneProvider_ShouldReturnWithValidDataLive(Provider provider, string ipAddress, bool expectSuccess)
        //{
        //    // Arrange
        //    var service = GetGeolocationService();

        //    // Act
        //    var result = await service.GetAsync(ipAddress, new Provider[] { provider }, CancellationToken.None);

        //    // Assert
        //    if (expectSuccess)
        //    {
        //        Assert.IsNotNull(result);
        //        Assert.IsNotNull(result.Location);
        //        Assert.AreEqual(provider, result.Provider);
        //        Assert.AreEqual(ipAddress, result.IpAddress);
        //    }
        //    else
        //    {
        //        Assert.IsNotNull(result);
        //        Assert.IsNull(result.Location);
        //        Assert.AreEqual(ipAddress, result.IpAddress);
        //    }
        //}

        [DynamicData(nameof(GetSourcesWithIpAddresses), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public async Task Service_WhenNoContent_ShouldIgnoreProvider(Provider provider, string ipAddress, bool expectSuccess)
        {
            // Arrange
            var service = GetGeolocationServiceFromServiceCollection(provider, HttpStatusCode.NoContent);

            // Act
            var result = await service.GetAsync(ipAddress, new [] { Provider.AbstractApi /* Priority 10 */, Provider.IpGeolocationApi /* Priority 50 */ }, CancellationToken.None);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Location);
            Assert.AreEqual(Provider.IpGeolocationApi, result.Provider);
            Assert.AreEqual(ipAddress, result.IpAddress);
        }

        [DynamicData(nameof(GetSourcesWithIpAddresses), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public async Task Service_WhenOneProvider_ShouldReturnWithValidData(Provider provider, string ipAddress, bool expectSuccess)
        {
            // Arrange
            var service = GetGeolocationServiceWithMockHttpMessageHandler(provider);

            // Act
            var result = await service.GetAsync(ipAddress, new [] { provider }, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Location);
            Assert.AreEqual(provider, result.Provider);
            Assert.AreEqual(ipAddress, result.IpAddress);
        }

        [DynamicData(nameof(GetSourcesWithIpAddresses), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public async Task Service_WhenNoProviders_ShouldReturnNullAndUnknown(Provider provider, string ipAddress, bool expectSuccess)
        {
            // Arrange
            var service = GetGeolocationServiceWithMockHttpMessageHandler(provider);

            // Act
            var result = await service.GetAsync(ipAddress, new Provider[] { }, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Location);
            Assert.AreEqual(Provider.Unknown, result.Provider);
            Assert.AreEqual(ipAddress, result.IpAddress);
        }

        [TestMethod]
        public async Task Service_WhenInvalidJson_ShouldReturnNullAndUnknown()
        {
            // Arrange
            var ipAddress = "127.0.0.1";
            var service = GetGeolocationServiceWithMockHttpMessageHandler("Invalid");

            // Act
            var result = await service.GetAsync(ipAddress, new Provider[] { Provider.AbstractApi }, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Location);
            Assert.AreEqual(Provider.Unknown, result.Provider);
            Assert.AreEqual(ipAddress, result.IpAddress);
        }

        private GeolocationService GetGeolocationServiceFromServiceCollection(Provider provider, HttpStatusCode firstStatusCode = HttpStatusCode.OK, HttpStatusCode statusCode = HttpStatusCode.OK)
        { 
            var services = new ServiceCollection();

            services
                .AddScoped(_ => Configuration)
                .AddGeolocationService(retryCount: 3)
                .AddHttpMessageHandler(() => new MockHttpMessageHandler(provider, firstStatusCode, statusCode));

            return services
                    .BuildServiceProvider()
                    .GetRequiredService<GeolocationService>();
        }

        private GeolocationService GetGeolocationService()
            => new GeolocationService(Configuration, new HttpClient(), null);

        private GeolocationService GetGeolocationServiceWithMockHttpMessageHandler(string provider, HttpStatusCode firstStatusCode = HttpStatusCode.OK, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new GeolocationService(Configuration, new HttpClient(GetMockHttpMessageHandler(provider, firstStatusCode, statusCode).Object), null);

        private GeolocationService GetGeolocationServiceWithMockHttpMessageHandler(Provider provider, HttpStatusCode firstStatusCode = HttpStatusCode.OK, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new GeolocationService(Configuration, new HttpClient(GetMockHttpMessageHandler($"{provider}", firstStatusCode, statusCode).Object), null);

        private Mock<HttpMessageHandler> GetMockHttpMessageHandler(string provider, HttpStatusCode? firstStatusCode, HttpStatusCode statusCode)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => GetMockResponse(request, provider, firstStatusCode, statusCode))
               .Verifiable();

            return handlerMock;
        }

        private Task<HttpResponseMessage> GetMockResponse(HttpRequestMessage request, string provider, HttpStatusCode? firstStatusCode, HttpStatusCode statusCode)
        {
            var context = request.GetPolicyExecutionContext();
            var retryCount = context.GetRetryCount();

            var content = TestData.GetJson(provider);
            var response = new HttpResponseMessage
            {
                StatusCode = retryCount > 0 ? statusCode : (firstStatusCode ?? statusCode),
                Content = new StringContent(content),
            };

            return Task.FromResult(response);
        }
    }
}
