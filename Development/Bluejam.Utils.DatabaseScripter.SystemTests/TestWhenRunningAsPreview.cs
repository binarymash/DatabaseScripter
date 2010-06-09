using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Bluejam.Utils.DatabaseScripter.Core;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    [TestFixture]
    public class TestWhenRunningAsPreview : TestBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            ConfigFileFactory.SetUpConfig("test.config", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Nominal.config");
        }

        [Test]
        public void Run()
        {
            Assert.AreEqual(ErrorCode.Ok, RunApplication("Bluejam.Utils.DatabaseScripter.exe", "-preview"));

            //database should still not exist
            Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

            //TODO: check preview.txt contains expected contents
        }
    }
}
