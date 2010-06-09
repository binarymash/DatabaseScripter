using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using Bluejam.Utils.DatabaseScripter.Core;
using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{

    [TestFixture]
    public class TestInvalidManifestSchema : AbstractTestBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            ConfigFileFactory.SetUpConfig(@"Example\Manifest.xml", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Manifest.InvalidSchema.xml");
        }

        [Test]
        public void Run()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var exeFile = directoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe"));
            Assert.IsNotNull(exeFile);
            Assert.AreEqual(ErrorCode.InvalidManifest, RunApplication(exeFile.FullName));
            Assert.IsFalse(server.Databases.Contains("MediaLibrary"));
        }

    }
}
