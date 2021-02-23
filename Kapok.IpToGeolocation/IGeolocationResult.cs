namespace Kapok.IpToGeolocation
{
    public interface IGeolocationResult
    {
        string IpAddress { get; set; }
        IGeolocationDto? Location { get; set; }
        Provider Provider { get; set; }
    }
}