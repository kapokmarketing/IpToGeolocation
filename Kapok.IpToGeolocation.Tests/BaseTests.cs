using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
