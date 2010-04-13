using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    [TestFixture]
    public class TestDatabaseScripter
    {

        #region Tests

        [Test]
        public void TestWhenRunningAsPreview()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["medialibrary"].ConnectionString))
            {
                connection.Open();

                var server = new Server(new ServerConnection(connection));

                //database should not exist
                Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

                RunApplication("-preview");

                //database should still not exist
                Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

                //TODO: check preview.txt contains expected contents

            }
        }

        [Test]
        public void Test()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["medialibrary"].ConnectionString))
            {
                connection.Open();
                var server = new Server(new ServerConnection(connection));

                try
                {
                    //database should not exist
                    Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

                    Assert.AreEqual(0, RunApplication());

                    //database should exist
                    Assert.IsTrue(server.Databases.Contains("MediaLibrary"));
                    var database = server.Databases["MediaLibrary"];
                    Assert.AreEqual("0.0.1", database.ExtendedProperties["SCHEMA_VERSION"].Value);
                    Assert.IsTrue(database.Tables.Contains("CodecType"));
                    Assert.IsTrue(database.Tables.Contains("Encoding"));
                }
                finally
                {
                    //make sure we've dropped the database
                    server.Databases["MediaLibrary"].Drop();
                }
            }

        }

        #endregion

        #region Helper methods

        private static int RunApplication(string arguments)
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "Bluejam.Utils.DatabaseScripter.exe";
            process.StartInfo.Arguments = arguments;

            var started = process.Start();
            process.WaitForExit();

            return process.ExitCode;
        }

        private static int RunApplication()
        {
            return RunApplication(string.Empty);
        }

        #endregion

    }
}
