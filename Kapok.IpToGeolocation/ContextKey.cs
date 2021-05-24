// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Kapok.IpToGeolocation
{
    public static class ContextKey
    {
        public static readonly string Provider = "provider";
        public static readonly string ProviderIndex = "index";
        public static readonly string ProvidersFailed = "failed";
        public static readonly string Handler = "handler";
        public static readonly string Request = "request";
        public static readonly string RetryCount = "retryCount";
    }
}
