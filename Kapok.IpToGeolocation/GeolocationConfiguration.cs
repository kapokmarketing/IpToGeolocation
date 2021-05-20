// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationConfiguration : IGeolocationConfiguration
    {
        public Dictionary<string, GeolocationProviderConfiguration> Providers { get; } = new Dictionary<string, GeolocationProviderConfiguration>();
    }
}