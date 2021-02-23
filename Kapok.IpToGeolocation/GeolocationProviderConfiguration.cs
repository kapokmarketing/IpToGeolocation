using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationProviderConfiguration
    {
        private string? _urlPatternWithPrivateKey;

        public Provider Name { get; set; }
        public bool Disabled { get; set; }
        public string? UrlPattern { get; set; }
        public string? PrivateKey { get; set; }
        public string? UrlPatternWithPrivateKey
            => _urlPatternWithPrivateKey ?? (_urlPatternWithPrivateKey = PrivateKey != null ? UrlPattern?.Replace("{Key}", PrivateKey) : UrlPattern);
    }
}