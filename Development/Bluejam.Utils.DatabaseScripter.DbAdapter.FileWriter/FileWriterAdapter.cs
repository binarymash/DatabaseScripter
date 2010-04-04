using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            FileName = "preview.txt";
        }

        #endregion

        #region IDatabaseAdapter Members

        public void Initialize()
        {
            fileWriter = System.IO.File.CreateText(FileName);
            fileWriter.AutoFlush = true;
        }

        /// <summary>
        /// Connects to the database using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public void Connect(string connectionString)
        {
            fileWriter.WriteLine(String.Format("--"));
            fileWriter.WriteLine(String.Format("-- Connect to {0}", connectionString));
            fileWriter.WriteLine(String.Format("--"));
        }

        /// <summary>
        /// Disconnects from the database
        /// </summary>
        public void Disconnect()
        {
            fileWriter.WriteLine(String.Format("--"));
            fileWriter.WriteLine(String.Format("-- Disconnect from database"));
            fileWriter.WriteLine(String.Format("--"));
        }

        public void BeginTransaction()
        {
            fileWriter.WriteLine(String.Format("--"));
            fileWriter.WriteLine(String.Format("-- Begin transaction"));
            fileWriter.WriteLine(String.Format("--"));
        }

        public void CommitTransaction()
        {
            fileWriter.WriteLine(String.Format("--"));
            fileWriter.WriteLine(String.Format("-- Commit transaction"));
            fileWriter.WriteLine(String.Format("--"));
        }

        public void RollBackTransaction()
        {
            fileWriter.WriteLine(String.Format("--"));
            fileWriter.WriteLine(String.Format("-- Roll back transaction"));
            fileWriter.WriteLine(String.Format("--"));
        }

        public void RunCommand(string databaseName, string command)
        {
            fileWriter.WriteLine();
            fileWriter.WriteLine(command);
            fileWriter.WriteLine();
        }

        public bool ConfirmVersion(string databaseName, Version version)
        {
            fileWriter.WriteLine(String.Format("--"));
            fileWriter.WriteLine(String.Format("-- Confirm current database version is {0}", version));
            fileWriter.WriteLine(String.Format("--"));

            return true;
        }

        public void SetVersion(string databaseName, Version version)
        {
            fileWriter.WriteLine(String.Format("--"));
            fileWriter.WriteLine(String.Format("-- Set database version - {0}", version.ToString()));
            fileWriter.WriteLine(String.Format("--"));
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
