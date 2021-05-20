// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kapok.IpToGeolocation.Tests
{
    public class BaseTests
    {
        protected static IConfiguration Configuration;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void InitializeBase(TestContext context)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        protected string GetJson(Provider source)
            => System.IO.File.ReadAllText($@"Data\{source}.json");

        protected string GetJson(string fileName)
            => System.IO.File.ReadAllText($@"Data\{fileName}.json");
        protected Stream GetJsonStream(Provider source)
            => System.IO.File.OpenRead($@"Data\{source}.json");

        protected Stream GetJsonStream(string fileName)
            => System.IO.File.OpenRead($@"Data\{fileName}.json");
    }
}
