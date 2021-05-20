// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Collections.Generic;

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationConfiguration
    {
        Dictionary<string, GeolocationProviderConfiguration> Providers { get; }
    }
}