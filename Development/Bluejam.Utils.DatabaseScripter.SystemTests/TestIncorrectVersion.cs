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
    public class TestIncorrectVersion : AbstractTestBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            ConfigFileFactory.SetUpConfig("Bluejam.Utils.DatabaseScripter.exe.config", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Config.IncorrectVersion.config");
        }

        [Test]
        public void Run()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var exeFile = directoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe"));
            Assert.IsNotNull(exeFile);
            Assert.AreEqual(ErrorCode.IncorrectCurrentVersion, RunApplication(exeFile.FullName));
            Assert.IsTrue(server.Databases.Contains("MediaLibrary"));
            var database = server.Databases["MediaLibrary"];
            Assert.AreEqual("0.0.0.0", database.ExtendedProperties["SCHEMA_VERSION"].Value);
            Assert.IsFalse(database.Tables.Contains("CodecType"));
            Assert.IsFalse(database.Tables.Contains("Encoding"));
            Assert.IsFalse(database.Tables.Contains("AnotherTable"));               
        }

    }
}
