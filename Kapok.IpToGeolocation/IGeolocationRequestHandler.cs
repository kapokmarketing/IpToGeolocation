// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Net.Http;

namespace Kapok.IpToGeolocation
{
    public interface IGeolocationRequestHandler
    {
        Provider SetRequestMessageUri(HttpRequestMessage requestMessage);
        Provider SetRequestMessageUri(HttpRequestMessage requestMessage, int retryCount);
    }
}