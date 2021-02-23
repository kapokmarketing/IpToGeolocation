using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpRegistry
{
    public class Currency
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("name_native")]
        public string? NameNative { get; set; }

        [JsonPropertyName("plural")]
        public string? Plural { get; set; }

        [JsonPropertyName("plural_native")]
        public string? PluralNative { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("symbol_native")]
        public string? SymbolNative { get; set; }

        [JsonPropertyName("format")]
        public Format? Format { get; set; }
    }
}