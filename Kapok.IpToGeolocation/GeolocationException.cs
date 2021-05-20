// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationException : Exception
    {
        public GeolocationException(string? message)
            : base(message)
        { }
    }
}