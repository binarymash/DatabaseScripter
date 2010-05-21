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
        /// <returns></returns>
        bool Initialize();

        /// <summary>
        /// Connects to the database using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        bool Connect(string connectionString);

        /// <summary>
        /// Disconnects from the database
        /// </summary>
        /// <returns></returns>
        bool Disconnect();

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns></returns>
        bool BeginTransaction();

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <returns></returns>
        bool CommitTransaction();

        /// <summary>
        /// Rolls the back transaction.
        /// </summary>
        /// <returns></returns>
        bool RollBackTransaction();

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        bool RunCommand(string databaseName, string command);

        /// <summary>
        /// Confirms the current version of the database matches the specified version.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="version">The version.</param>
        /// <param name="confirmed">if set to <c>true</c>, the database version has been confirmed.</param>
        /// <returns></returns>
        bool ConfirmVersion(string databaseName, Version version, out bool confirmed);

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        bool SetVersion(string databaseName, Version version);

    }

}
