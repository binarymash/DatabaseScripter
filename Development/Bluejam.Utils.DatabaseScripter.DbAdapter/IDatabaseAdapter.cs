using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.DbAdapter
{
    public interface IDatabaseAdapter : IDisposable
    {

        /// <summary>
        /// Initialises the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        void Initialise(string connectionString);
            
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
        /// Gets the version.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns></returns>
        Version GetVersion(string databaseName);

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="version">The version.</param>
        void SetVersion(string databaseName, Version version);

    }

}
