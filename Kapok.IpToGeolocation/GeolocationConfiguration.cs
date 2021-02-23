using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationConfiguration : IGeolocationConfiguration
    {
        public List<GeolocationProviderConfiguration> Providers { get; } = new List<GeolocationProviderConfiguration>();
    }
}