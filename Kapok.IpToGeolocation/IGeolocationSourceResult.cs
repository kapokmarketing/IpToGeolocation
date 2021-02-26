namespace Kapok.IpToGeolocation
{
    public interface IGeolocationSourceResult : IGeolocationDto
    {
        Provider Source { get; }
        bool Success { get; }
    }
}