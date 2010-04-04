using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.DbAdapter
{
    public interface IDatabaseAdapter : IDisposable
    {

        /// <summary>
        /// Initializes the adapter.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Connects to the database using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        void Connect(string connectionString);

        /// <summary>
        /// Disconnects from the database
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        void Disconnect();

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls the back transaction.
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="command">The command.</param>
        void RunCommand(string databaseName, string command);

        /// <summary>
        /// Confirms the current version of the database matches the specified version.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        bool ConfirmVersion(string databaseName, Version version);

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="version">The version.</param>
        void SetVersion(string databaseName, Version version);

    }

}
