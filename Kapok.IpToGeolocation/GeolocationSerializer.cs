// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

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

        //   T:System.ArgumentNullException:
        //     utf8Json or returnType is null.
        //
        //   T:System.Text.Json.JsonException:
        //     The JSON is invalid. -or- TValue is not compatible with the JSON. -or- There
        //     is remaining data in the stream.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="utf8Json"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when utf8Json or returnType is null.</exception>
        /// <exception cref="JsonException">Thrown when the JSON is invalid or there is remaining data in the stream.</exception>
        public static async Task<IGeolocationDto?> DeserializeAsync(Provider source, Stream utf8Json, CancellationToken cancellationToken)
        {
            var returnType = GetType(source);
            var result = (IGeolocationSourceResult) await JsonSerializer.DeserializeAsync(utf8Json, returnType, cancellationToken: cancellationToken);

            return result.Success ? GeolocationDto.Create(result) : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="utf8Json"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when utf8Json or returnType is null.</exception>
        /// <exception cref="JsonException">Thrown when the JSON is invalid or there is remaining data in the string.</exception>
        public static IGeolocationDto Deserialize(Provider source, string json)
        {
            var returnType = GetType(source);
            var result = (IGeolocationDto) JsonSerializer.Deserialize(json, returnType);

            return GeolocationDto.Create(result);
        }
    }
}
