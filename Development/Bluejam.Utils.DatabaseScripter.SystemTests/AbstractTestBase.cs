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

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{

    /// <summary>
    /// Base class for system test cases
    /// </summary>
    public abstract class AbstractTestBase
    {


        [SetUp]
        public virtual void SetUp()
        {
            Console.WriteLine(string.Format("Running {0}", this.GetType().Name));

            var connectionStringConfig = new KeyValuePair<string, string>("/configuration/connectionStrings/add[@name='medialibrary']/@connectionString", ConfigurationManager.ConnectionStrings["nominal"].ConnectionString);
            ConfigFileFactory.SetUpConfig("DatabaseScripter.exe.config", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Config.Nominal.config", new List<KeyValuePair<string, string>>{connectionStringConfig});
            ConfigFileFactory.SetUpConfig(@"Example\manifest.xml", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Manifest.Nominal.xml");
            ConfigFileFactory.SetUpConfig(@"Example\EnvironmentConfigurations\SystemTest.xml", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Environment.Nominal.xml");

            connection = new SqlConnection(connectionStringConfig.Value);
            connection.Open();
            server = new Server(new ServerConnection(connection));

            //database should not exist
            Assert.IsFalse(server.Databases.Contains("MediaLibrary"));
        }

        [TearDown]
        public virtual void TearDown()
        {
            //make sure we've dropped the database
            try
            {
                server.Databases["MediaLibrary"].Drop();
            }
            catch
            {

            }
            connection.Close();

        }

        protected static Domain.ErrorCode RunApplication(string exePath, string arguments)
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = arguments;

            var started = process.Start();
            process.WaitForExit();

            return (Domain.ErrorCode)process.ExitCode;
        }

        protected static Domain.ErrorCode RunApplication(string exePath)
        {
            return RunApplication(exePath, string.Empty);
        }

        protected SqlConnection connection;
        protected Server server;

    }
}
