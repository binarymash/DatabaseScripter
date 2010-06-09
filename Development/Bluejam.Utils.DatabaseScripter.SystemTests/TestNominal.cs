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
    public class TestNominal : AbstractTestBase
    {

        [Test]
        public void Run()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var exeFile = directoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe"));
            Assert.IsNotNull(exeFile);

            Assert.AreEqual(ErrorCode.Ok, RunApplication(exeFile.FullName));

            //database should exist
            Assert.IsTrue(server.Databases.Contains("MediaLibrary"));
            var database = server.Databases["MediaLibrary"];
            Assert.AreEqual("0.0.0.1", database.ExtendedProperties["SCHEMA_VERSION"].Value);
            Assert.IsTrue(database.Tables.Contains("CodecType"));
            Assert.IsTrue(database.Tables.Contains("Encoding"));               
        }
    }
}
