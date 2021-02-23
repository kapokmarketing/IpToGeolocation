using System;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationException : Exception
    {
        public GeolocationException(string? message)
            : base(message)
        { }
    }
}