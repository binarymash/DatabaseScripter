using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Services
{
    [TestFixture]
    public class TestLicenceService
    {
        [Test]
        public void TestLicenseSplash()
        {
            var service = new Bluejam.Utils.DatabaseScripter.Services.LicenceService();
            var expectedLicenseSplash = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{1}DatabaseScripter version {0}  Copyright (C) 2010  Philip Wood {1}http://code.google.com/p/databasescripter/{1}{1}This program comes with ABSOLUTELY NO WARRANTY. This is free software, {1}and you are welcome to redistribute it under certain conditions. {1}{1}For more information, see License.txt.{1}{1}{1}",
                 Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                 System.Environment.NewLine);
            Assert.That(service.LicenceSplash, Is.EqualTo(expectedLicenseSplash));
        }
    }
}
