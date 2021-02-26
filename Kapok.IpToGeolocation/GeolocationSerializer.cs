using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Kapok.IpToGeolocation
{
    public static class GeolocationSerializer
    {
        private static Type GetType(Provider source)
        => source switch
        {
            Provider.AbstractApi => typeof(AbstractApi.Result),
            Provider.IpRegistry => typeof(IpRegistry.Result),
            Provider.IpStack => typeof(IpStack.Result),
            Provider.IpGeolocationApi => typeof(IpGeolocationApi.Result),
            _ => throw new NotImplementedException(),
        };

        public static async Task<IGeolocationDto?> DeserializeAsync(Provider source, Stream utf8Json, CancellationToken cancellationToken)
        {
            var returnType = GetType(source);
            var result = (IGeolocationSourceResult) await JsonSerializer.DeserializeAsync(utf8Json, returnType, cancellationToken: cancellationToken);

            return result.Success ? GeolocationDto.Create(result) : null;
        }

        public static IGeolocationDto Deserialize(Provider source, string json)
        {
            var returnType = GetType(source);
            var result = (IGeolocationDto) JsonSerializer.Deserialize(json, returnType);

            return GeolocationDto.Create(result);
        }
    }
}