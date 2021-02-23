using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.AbstractApi
{
    public class Connection
    {
        [JsonPropertyName("autonomous_system_number")]
        public int? AutonomousSystemNumber { get; set; }

        [JsonPropertyName("autonomous_system_organization")]
        public string? AutonomousSystemOrganization { get; set; }

        [JsonPropertyName("connection_type")]
        public string? ConnectionType { get; set; }

        [JsonPropertyName("isp_name")]
        public string? IspName { get; set; }

        [JsonPropertyName("organization_name")]
        public string? OrganizationName { get; set; }
    }
}
