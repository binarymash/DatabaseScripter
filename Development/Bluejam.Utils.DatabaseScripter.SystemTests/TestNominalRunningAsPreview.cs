using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using Bluejam.Utils.DatabaseScripter.Core;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    [TestFixture]
    public class TestNominalRunningAsPreview : AbstractTestBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            ConfigFileFactory.SetUpConfig("test.config", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Config.Nominal.config");
        }

        [Test]
        public void Run()
        {
            Assert.AreEqual(ErrorCode.Ok, RunApplication("Bluejam.Utils.DatabaseScripter.exe", "-preview"));

            //database should still not exist
            Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

            //assert generated preview file
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("Bluejam.Utils.DatabaseScripter.SystemTests.Files.Asserts.NominalPreview.txt");
            var streamReader = new StreamReader(stream);

            var generatedPreview = File.ReadAllText("preview.txt");
            var expectedPreview = streamReader.ReadToEnd();
            
            Assert.AreEqual(expectedPreview, generatedPreview);

        }
    }
}
