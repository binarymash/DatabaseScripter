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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    [TestFixture]
    public class TestNominalRunningAsPreview : AbstractTestBase
    {

        [Test]
        public void Run()
        {
            Assert.AreEqual(Domain.Interfaces.ErrorCode.Ok, RunApplication("DatabaseScripter.exe", "--preview --environment=SystemTest --scripts=create,\"increment to 0.0.0.1\",\"insert sample data\""));

            //database should still not exist
            Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

            //assert generated preview file
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("Bluejam.Utils.DatabaseScripter.SystemTests.Files.Asserts.NominalPreview.txt");
            var streamReader = new StreamReader(stream);

            var generatedPreview = File.ReadAllText("preview.txt");
            var expectedPreview = streamReader.ReadToEnd();
            
            Assert.AreEqual(expectedPreview, generatedPreview);
            dbAsserter.AssertThatDatabaseDoesNotExist();

        }
    }
}
