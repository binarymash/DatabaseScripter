//DatabaseScripter  Copyright (C) 2011  Philip Wood
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
using System.Data.SqlClient;
using System.Globalization;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

using log4net;


namespace Bluejam.Utils.DatabaseScripter.DbAdapter.SqlServerSmo
{
    public class Adapter : Domain.Strategies.DatabaseAdapter
    {

        #region Non-public

        private static readonly ILog log = LogManager.GetLogger(typeof(Adapter));
        private SqlConnection _connection;
        private Server _server;

        #endregion

        #region Constructors

        public Adapter()
        {
        }
        
        #endregion

        #region DatabaseAdapter overrides

        /// <summary>
        /// Initializes the adapter.
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// Connects the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public override bool Connect(string connectionString)
        {
            var success = false;

            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();

                _server = new Server(new ServerConnection(_connection));

                success = true;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                log.Error("InvalidOperationException when establishing database connection. Check the debug information that follows.", invalidOperationException);
            }
            catch (SqlException sqlException)
            {
                log.Error("SqlException when establishing database connection. Check the debug information that follows.", sqlException);
            }
            catch (ArgumentException argumentException)
            {
                log.Error("ArgumentException when establishing database connection. Check the debug information that follows.", argumentException);
            }

            return success;
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public override bool Disconnect()
        {
            try
            {
                _connection.Close();
            }
            catch (SqlException sqlException)
            {
                log.Error("SqlException when establishing database connection. Check the debug information that follows.", sqlException);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Begins the transaction on the database.
        /// </summary>
        public override bool BeginTransaction()
        {
            try
            {
                _server.ConnectionContext.BeginTransaction();
            }
            catch (ExecutionFailureException efeEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", efeEx);
                return false;
            }
            catch (SmoException smoEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", smoEx);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Commits the transaction to the database.
        /// </summary>
        /// <returns></returns>
        public override bool CommitTransaction()
        {
            try
            {
                _server.ConnectionContext.CommitTransaction();
            }
            catch (ExecutionFailureException efeEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", efeEx);
                return false;
            }
            catch (SmoException smoEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", smoEx);
                return false;
            }

            return true;
        }

        public override bool RollBackTransaction()
        {
            try
            {
                _server.ConnectionContext.RollBackTransaction();
            }
            catch (ExecutionFailureException efeEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", efeEx);
                return false;
            }
            catch (SmoException smoEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", smoEx);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Runs the command and sets the new version number.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="newVersion">The new version.</param>
        /// <returns></returns>
        public override bool RunCommand(string databaseName, string command)
        {
            try
            {
                _server.ConnectionContext.ExecuteNonQuery(command);
            }
            catch (ExecutionFailureException efeEx)
            {
                log.Error("An SmoException occurred. See the debug information that follows.", efeEx);
                return false;
            }
            catch (SmoException smoEx)
            {
                log.Error("An SmoException occurred. See the debug information that follows.", smoEx);
                return false;
            }

            return true;

        }

        /// <summary>
        /// Confirms the current version of the database matches the specified version.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="version">The version.</param>
        /// <param name="confirmed">if set to <c>true</c>, the database version has been confirmed.</param>
        /// <returns></returns>
        public override bool ConfirmVersion(string databaseName, Domain.Values.Version version, out bool confirmed)
        {
            confirmed = false;

            try
            {
                var database = _server.Databases[databaseName];
                if (null == database)
                {
                    log.ErrorFormat("There is no database called '{0}' on the server.", databaseName);
                    return false;
                }

                var currentDatabaseVersion = new Domain.Values.Version(database.ExtendedProperties["SCHEMA_VERSION"].Value as string);
                log.InfoFormat(CultureInfo.InvariantCulture, "Current database version is {0}", currentDatabaseVersion);
                confirmed = (version == currentDatabaseVersion);
            }
            catch (ExecutionFailureException efeEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", efeEx);
                return false;
            }
            catch (SmoException smoEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", smoEx);
                return false;
            }

            return true;

        }

        /// <summary>
        /// Sets the schema version in the database.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns></returns>
        public override bool SetVersion(string databaseName, Domain.Values.Version version)
        {
            try
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
            catch (ExecutionFailureException efeEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", efeEx);
                return false;
            }
            catch (SmoException smoEx)
            {
                log.Error("An Exception occurred. See the debug information that follows.", smoEx);
                return false;
            }

            return true;
        }

        #endregion

        #region IDisposable implementation

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                _connection.Close();
            }

            _connection = null;

            base.Dispose(disposing);
        }

        // Use C# destructor syntax for finalization code.
        ~Adapter()
        {
            Dispose(false);
        }

        #endregion

    }
}
