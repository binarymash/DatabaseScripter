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
            Assert.AreEqual(ErrorCode.Ok, RunApplication("DatabaseScripter.exe", "-preview"));

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
