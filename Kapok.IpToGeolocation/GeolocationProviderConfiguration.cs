// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation
{
    public class GeolocationProviderConfiguration
    {
        private string? _urlPatternWithPrivateKey;

        public Provider Name { get; set; }
        public bool Disabled { get; set; }
        public int Priority { get; set; }
        public string? UrlPattern { get; set; }
        public string? PrivateKey { get; set; }
        public string? UrlPatternWithPrivateKey
            => _urlPatternWithPrivateKey ??= PrivateKey != null ? UrlPattern?.Replace("{Key}", PrivateKey) : UrlPattern;
    }
}
