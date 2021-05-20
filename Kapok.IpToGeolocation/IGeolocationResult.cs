// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationResult
    {
        string IpAddress { get; set; }
        IGeolocationDto? Location { get; set; }
        Provider Provider { get; set; }
    }
}