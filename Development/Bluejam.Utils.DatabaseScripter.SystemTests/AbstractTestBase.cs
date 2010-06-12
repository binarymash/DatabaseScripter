﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Bluejam.Utils.DatabaseScripter.Core;

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
            var connectionStringConfig = new KeyValuePair<string, string>("/configuration/connectionStrings/add[@name='medialibrary']/@connectionString", ConfigurationManager.ConnectionStrings["nominal"].ConnectionString);
            ConfigFileFactory.SetUpConfig("Bluejam.Utils.DatabaseScripter.exe.config", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Config.Nominal.config", new List<KeyValuePair<string, string>>{connectionStringConfig});
            ConfigFileFactory.SetUpConfig(@"Example\manifest.xml", "Bluejam.Utils.DatabaseScripter.SystemTests.Files.Manifest.Nominal.xml");

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

        protected static ErrorCode RunApplication(string exePath, string arguments)
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = arguments;

            var started = process.Start();
            process.WaitForExit();

            return (ErrorCode)process.ExitCode;
        }

        protected static ErrorCode RunApplication(string exePath)
        {
            return RunApplication(exePath, string.Empty);
        }

        protected SqlConnection connection;
        protected Server server;

    }
}
