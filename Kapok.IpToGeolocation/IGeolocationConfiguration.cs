using System.Collections.Generic;

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationConfiguration
    {
        Dictionary<string, GeolocationProviderConfiguration> Providers { get; }
    }
}