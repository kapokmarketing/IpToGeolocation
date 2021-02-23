using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationConfiguration : IGeolocationConfiguration
    {
        public Dictionary<string, GeolocationProviderConfiguration> Providers { get; } = new Dictionary<string, GeolocationProviderConfiguration>();
    }
}