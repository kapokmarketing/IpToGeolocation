using System.Collections.Generic;

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationConfiguration
    {
        List<GeolocationProviderConfiguration> Providers { get; }
    }
}