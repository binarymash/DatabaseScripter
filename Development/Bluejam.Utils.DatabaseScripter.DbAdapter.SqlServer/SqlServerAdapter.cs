using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

using Bluejam.Utils.DatabaseScripter.DbAdapter;

namespace Bluejam.Utils.DatabaseScripter.DbAdapter.SqlServer
{
    public class SqlServerAdapter : IDatabaseAdapter
    {

        #region Non-public

        private SqlConnection _connection;
        private Server _server;

        #endregion

        #region Constructors

        public SqlServerAdapter()
        {
        }
        
        #endregion

        #region IDatabaseAdapter implementation

        public void Initialize(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _server = new Server(new ServerConnection(_connection));
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            _server.ConnectionContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _server.ConnectionContext.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _server.ConnectionContext.RollBackTransaction();
        }

        /// <summary>
        /// Runs the command and sets the new version number.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="newVersion">The new version.</param>
        /// <returns></returns>
        public void RunCommand(string databaseName, string command)
        {
            _server.ConnectionContext.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <returns></returns>
        public Version GetVersion(string databaseName)
        {
            return new Version(_server.Databases[databaseName].ExtendedProperties["SCHEMA_VERSION"].Value as string);
        }

        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public void SetVersion(string databaseName, Version version)
        {
            if (_server.Databases[databaseName].ExtendedProperties.Contains("SCHEMA_VERSION"))
            {
                var extendedProperty = _server.Databases[databaseName].ExtendedProperties["SCHEMA_VERSION"];
                extendedProperty.Value = version.ToString();
                extendedProperty.Alter();
            }
            else
            {
                var extendedProperty = new ExtendedProperty(_server.Databases[databaseName], "SCHEMA_VERSION", version.ToString());
                extendedProperty.Create();

                _server.Databases[databaseName].Alter();
            }
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection.Close();
            }

            _connection = null;
        }

        // Use C# destructor syntax for finalization code.
        ~SqlServerAdapter()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion

    }
}
