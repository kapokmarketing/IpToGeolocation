using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kapok.IpToGeolocation.Tests
{
    [TestClass]
    [DeploymentItem("Data")]
    public class JsonTests : BaseTests
    {
        private static IEnumerable<Provider> Sources;

        [ClassInitialize()]
        public static void Initialize(TestContext context)
        {
            Sources = new Provider[] { Provider.AbstractApi, Provider.IpRegistry, Provider.IpStack };
        }

        public static IEnumerable<object[]> GetSources()
            => Sources.Select(source => new object[] { source });

        public static IEnumerable<object[]> GetSourcesWithCities()
        {
            yield return new object[] { Provider.AbstractApi, "Modesto" };
            yield return new object[] { Provider.IpRegistry, "St. Petersburg" };
            yield return new object[] { Provider.IpStack, "Los Angeles" };
        }

        [DynamicData(nameof(GetSources), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void JsonData_ShouldDeserializeWithData(Provider source)
        {
            var json = GetJson(source);
            var result = GeolocationSerializer.Deserialize(source, json);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.City);
            Assert.IsNotNull(result.RegionCode);
            Assert.IsNotNull(result.RegionName);
            Assert.IsNotNull(result.CountryCode);
            Assert.IsNotNull(result.CountryName);
            Assert.IsNotNull(result.Latitude);
            Assert.IsNotNull(result.Longitude);
        }

        [DynamicData(nameof(GetSourcesWithCities), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void JsonData_ShouldDeserializeWithCorrectCity(Provider source, string expectedValue)
        {
            var json = GetJson(source);
            var result = GeolocationSerializer.Deserialize(source, json);

            Assert.AreEqual(expectedValue, result?.City);
        }
    }
}
