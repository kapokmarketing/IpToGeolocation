using System.Net.Http;

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationRequestHandler
    {
        Provider SetRequestMessageUri(HttpRequestMessage requestMessage);
        Provider SetRequestMessageUri(HttpRequestMessage requestMessage, int retryCount);
    }
}