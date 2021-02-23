using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.AbstractApi
{
    public class Security
    {
        [JsonPropertyName("is_vpn")]
        public bool? IsVpn { get; set; }
    }
}
