using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Bluejam.Utils.DatabaseScripter.DbAdapter.FileWriter
{
    public class FileWriterAdapter : IDatabaseAdapter
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the expected version.
        /// </summary>
        /// <value>The expected version.</value>
        public Version ExpectedVersion { get; set; }

        #endregion

        #region Constructors

        public FileWriterAdapter()
        {
            var execPath = new FileInfo(Assembly.GetExecutingAssembly().Location);
            FileName = Path.Combine(execPath.Directory.FullName, "preview.txt");
        }

        #endregion

        #region IDatabaseAdapter Members

        public bool Initialize()
        {
            fileWriter = System.IO.File.CreateText(FileName);
            fileWriter.AutoFlush = true;

            return true;
        }

        /// <summary>
        /// Connects to the database using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public bool Connect(string connectionString)
        {
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "-- Connect to {0}", connectionString));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));

            return true;
        }

        /// <summary>
        /// Disconnects from the database
        /// </summary>
        public bool Disconnect()
        {
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "-- Disconnect from database"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));

            return true;
        }

        public bool BeginTransaction()
        {
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "-- Begin transaction"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));

            return true;
        }

        public bool CommitTransaction()
        {
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "-- Commit transaction"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));

            return true;
        }

        public bool RollBackTransaction()
        {
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "-- Roll back transaction"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));

            return true;
        }

        public bool RunCommand(string databaseName, string command)
        {
            fileWriter.WriteLine();
            fileWriter.WriteLine(command);
            fileWriter.WriteLine();

            return true;
        }

        public bool ConfirmVersion(string databaseName, Version version, out bool confirmed)
        {
            confirmed = true;

            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "-- Confirm current database version is {0}", version));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));

            return true;
        }

        public bool SetVersion(string databaseName, Version version)
        {
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "-- Set database version - {0}", version.ToString()));
            fileWriter.WriteLine(String.Format(CultureInfo.InvariantCulture, "--"));

            return true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            fileWriter.Close();
        }

        #endregion

        #region Non-public

        System.IO.StreamWriter fileWriter;

        #endregion

    }
}
