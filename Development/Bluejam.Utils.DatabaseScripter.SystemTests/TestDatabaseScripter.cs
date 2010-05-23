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

                RunApplication("Bluejam.Utils.DatabaseScripter.exe", "-preview");

                //database should still not exist
                Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

                //TODO: check preview.txt contains expected contents

            }
        }

        [Test]
        public void Test()
        {
            var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            var exeFile = directoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe"));
            Assert.IsNotNull(exeFile);
            Test(exeFile.FullName);
        }

        [Test]
        public void TestWhenNotInCurrentWorkingDirectory()
        {
            var originalDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            Assert.IsNotNull(originalDirectoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe")));
            try
            {
                var newDirectoryInfo = new DirectoryInfo(Path.Combine(originalDirectoryInfo.FullName, ".."));
                Directory.SetCurrentDirectory(newDirectoryInfo.FullName);
                Assert.IsNull(newDirectoryInfo.GetFiles().ToList().Find(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe")));
                var exeFile = originalDirectoryInfo.GetFiles().First(fileInfo => fileInfo.Name.Equals("Bluejam.Utils.DatabaseScripter.exe"));
                Test(exeFile.FullName);
            }
            finally
            {
                Directory.SetCurrentDirectory(originalDirectoryInfo.FullName);
            }
        }


        #endregion

        #region Helper methods

        private static void Test(string pathToExe)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["medialibrary"].ConnectionString))
            {
                connection.Open();
                var server = new Server(new ServerConnection(connection));

                try
                {
                    //database should not exist
                    Assert.IsFalse(server.Databases.Contains("MediaLibrary"));

                    Assert.AreEqual(0, RunApplication(pathToExe));

                    //database should exist
                    Assert.IsTrue(server.Databases.Contains("MediaLibrary"));
                    var database = server.Databases["MediaLibrary"];
                    Assert.AreEqual("0.0.0.1", database.ExtendedProperties["SCHEMA_VERSION"].Value);
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

        private static int RunApplication(string exePath, string arguments)
        {
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = exePath;
            process.StartInfo.Arguments = arguments;

            var started = process.Start();
            process.WaitForExit();

            return process.ExitCode;
        }

        private static int RunApplication(string exePath)
        {
            return RunApplication(exePath, string.Empty);
        }

        #endregion

    }
}
