using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bluejam.Utils.DatabaseScripter.Core;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    [TestFixture]
    public class TestNominalWhenNotRunningInExeDir : TestBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            ConfigFileFactory.SetUpConfig("Bluejam.Utils.DatabaseScripter.exe.config", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Nominal.config");
        }

        [Test]
        public void Run()
        {
            var originalDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            Assert.IsNotNull(originalDirectoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe")));
            try
            {
                var newDirectoryInfo = new DirectoryInfo(Path.Combine(originalDirectoryInfo.FullName, ".."));
                Directory.SetCurrentDirectory(newDirectoryInfo.FullName);
                Assert.IsNull(newDirectoryInfo.GetFiles().ToList().Find(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe")));
                var exeFile = originalDirectoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe"));

                Assert.AreEqual(ErrorCode.Ok, RunApplication(exeFile.FullName));

                //database should exist
                Assert.IsTrue(server.Databases.Contains("MediaLibrary"));
                var database = server.Databases["MediaLibrary"];
                Assert.AreEqual("0.0.0.1", database.ExtendedProperties["SCHEMA_VERSION"].Value);
                Assert.IsTrue(database.Tables.Contains("CodecType"));
                Assert.IsTrue(database.Tables.Contains("Encoding"));               
            }
            finally
            {
                Directory.SetCurrentDirectory(originalDirectoryInfo.FullName);
            }
        }

    }
}
