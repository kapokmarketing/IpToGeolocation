// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
