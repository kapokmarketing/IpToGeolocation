// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kapok.IpToGeolocation.Tests
{
    public static class TestData
    {
        public static string GetJson(Provider source)
            => System.IO.File.ReadAllText($@"Data\{source}.json");

        public static string GetJson(string fileName)
            => System.IO.File.ReadAllText($@"Data\{fileName}.json");
        public static Stream GetJsonStream(Provider source)
            => System.IO.File.OpenRead($@"Data\{source}.json");

        public static Stream GetJsonStream(string fileName)
            => System.IO.File.OpenRead($@"Data\{fileName}.json");
    }
}
