//DatabaseScripter  Copyright (C) 2011  Philip Wood
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
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{

    [TestFixture]
    public class TestWhenInvalidManifestSchema : AbstractTestBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            ConfigFileFactory.SetUpConfig(@"Example\Manifest\Manifest.xml", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Manifest.InvalidSchema.xml");
        }

        [Test]
        public void Run()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var exeFile = directoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("DatabaseScripter.exe"));
            Assert.IsNotNull(exeFile);
            Assert.AreEqual(Domain.ErrorCode.InvalidManifest, RunApplication(exeFile.FullName, "--environment=SystemTest --scripts=create"));
            dbAsserter.AssertThatDatabaseDoesNotExist();
        }

    }
}
