//DatabaseScripter  Copyright (C) 2010  Philip Wood
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    [TestFixture]
    public class TestNominalWhenNotRunningInExeDir : AbstractTestBase
    {

        [Test]
        public void Run()
        {
            var originalDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            Assert.IsNotNull(originalDirectoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("DatabaseScripter.exe")));
            try
            {
                var newDirectoryInfo = new DirectoryInfo(Path.Combine(originalDirectoryInfo.FullName, ".."));
                Directory.SetCurrentDirectory(newDirectoryInfo.FullName);
                Assert.IsNull(newDirectoryInfo.GetFiles().ToList().Find(fileInfo => fileInfo.Name.Equals("DatabaseScripter.exe")));
                var exeFile = originalDirectoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("DatabaseScripter.exe"));

                Assert.AreEqual(Domain.ErrorCode.Ok, RunApplication(exeFile.FullName, "--environment=SystemTest --scripts=create,\"increment to 0.0.0.1\""));

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
