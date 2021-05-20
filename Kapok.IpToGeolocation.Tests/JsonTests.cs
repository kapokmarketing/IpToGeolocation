// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kapok.IpToGeolocation.Tests
{
    [TestClass]
    [DeploymentItem("Data")]
    public class JsonTests : BaseTests
    {
        private static IEnumerable<Provider> s_sources;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            s_sources = new Provider[] { Provider.AbstractApi, Provider.IpRegistry, Provider.IpStack };
        }

        public static IEnumerable<object[]> GetSources()
            => s_sources.Select(source => new object[] { source });

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
            // Arrange
            var json = GetJson(source);

            // Act
            var result = GeolocationSerializer.Deserialize(source, json);

            // Assert
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
            // Arrange
            var json = GetJson(source);

            // Act
            var result = GeolocationSerializer.Deserialize(source, json);

            // Assert
            Assert.AreEqual(expectedValue, result?.City);
        }

        [DynamicData(nameof(GetSourcesWithCities), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public async Task JsonData_ShouldDeserializeAsyncWithCorrectCity(Provider source, string expectedValue)
        {
            // Arrange
            using var jsonStream = GetJsonStream(source);

            // Act
            var result = await GeolocationSerializer.DeserializeAsync(source, jsonStream, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedValue, result?.City);
        }

        [TestMethod]
        public void JsonData_WhenInvalidJson_ShouldThrownJsonException()
        {
            // Arrange
            var json = GetJson("Invalid");

            // Act
            Action action = () =>
            {
                var result = GeolocationSerializer.Deserialize(Provider.AbstractApi, json);
            };

            // Assert
            Assert.ThrowsException<JsonException>(action);
        }

        [TestMethod]
        public async Task JsonData_WhenInvalidJson_ShouldThrownJsonExceptionAsync()
        {
            // Arrange
            using var jsonStream = GetJsonStream("Invalid");

            // Act
            Func<Task> action = async () =>
            {
                var result = await GeolocationSerializer.DeserializeAsync(Provider.AbstractApi, jsonStream, CancellationToken.None);
            };

            // Assert
            await Assert.ThrowsExceptionAsync<JsonException>(action);
        }
    }
}
