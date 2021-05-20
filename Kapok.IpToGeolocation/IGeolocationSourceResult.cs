// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationSourceResult : IGeolocationDto
    {
        Provider Source { get; }
        bool Success { get; }
    }
}