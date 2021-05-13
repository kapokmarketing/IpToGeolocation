using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kapok.IpToGeolocation.Tests
{
    [TestClass]
    [DeploymentItem("Data")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Unused parameters simplifies DynamicData usage.")]
    public class ServiceTests : BaseTests
    {

        public static IEnumerable<object[]> GetSourcesWithIpAddresses()
        {
            yield return new object[] { Provider.IpGeolocationApi, "91.213.103.0", true };
            yield return new object[] { Provider.AbstractApi, "166.171.248.255", false };
        }

        [DynamicData(nameof(GetSourcesWithIpAddresses), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public async Task Service_WhenOneProvider_ShouldReturnWithValidDataLive(Provider provider, string ipAddress, bool expectSuccess)
        {
            // Arrange
            var service = GetGeolocationService();

            // Act
            var result = await service.GetAsync(ipAddress, new Provider[] { provider }, CancellationToken.None);

            // Assert
            if (expectSuccess)
            {
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Location);
                Assert.AreEqual(provider, result.Provider);
                Assert.AreEqual(ipAddress, result.IpAddress);
            }
            else
            {
                Assert.IsNotNull(result);
                Assert.IsNull(result.Location);
                // TODO: Should we check the Provider?
                Assert.AreEqual(ipAddress, result.IpAddress);
            }
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

        private GeolocationService GetGeolocationService()
            => new GeolocationService(Configuration, new HttpClient(), null);

        private GeolocationService GetGeolocationServiceWithMockHttpMessageHandler(Provider provider)
            => new GeolocationService(Configuration, new HttpClient(GetMockHttpMessageHandler(provider).Object), null);

        private Mock<HttpMessageHandler> GetMockHttpMessageHandler(Provider provider)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => GetMockResponse(provider))
               .Verifiable();

            return handlerMock;
        }

        private Task<HttpResponseMessage> GetMockResponse(Provider provider)
        {
            var content = GetJson(provider);
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content),
            };

            return Task.FromResult(response);
        }
    }
}
