using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.AbstractApi
{
    public class Currency
    {
        [JsonPropertyName("currency_name")]
        public string? CurrencyName { get; set; }

        [JsonPropertyName("currency_code")]
        public string? CurrencyCode { get; set; }
    }
}
