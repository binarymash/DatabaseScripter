//DatabaseScripter  Copyright (C) 2010  Philip Wood
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
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Domain
{
    public class Script
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; protected set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>The name of the database.</value>
        public string DatabaseName { get; protected set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether [wrap in transaction].
        /// </summary>
        /// <value><c>true</c> if [wrap in transaction]; otherwise, <c>false</c>.</value>
        public bool WrapInTransaction { get; protected set; }

        /// <summary>
        /// Gets or sets the current version.
        /// </summary>
        /// <value>The current version.</value>
        public Version CurrentVersion { get; protected set; }

        /// <summary>
        /// Gets or sets the new version.
        /// </summary>
        /// <value>The new version.</value>
        public Version NewVersion { get; protected set; }

        #endregion

        #region Constructors

        public Script(string name, string description, string databaseName, string connectionString, bool wrapInTransaction, Version currentVersion, Version newVersion, string command)
        {
            Name = name;
            Description = description;
            DatabaseName = databaseName;
            ConnectionString = connectionString;
            WrapInTransaction = wrapInTransaction;
            CurrentVersion = currentVersion;
            NewVersion = newVersion;
            Command = command;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Runs the specified database adapter.
        /// </summary>
        /// <param name="databaseAdapter">The database adapter.</param>
        /// <returns></returns>
        public ErrorCode Run(DatabaseAdapter databaseAdapter)
        {
            log.InfoFormat(CultureInfo.InvariantCulture, "{0} : running...", this);

            var errorCode = ErrorCode.Ok;

            if (!databaseAdapter.Connect(ConnectionString))
            {
                log.Error("Failed to connect to the database");
                errorCode = ErrorCode.DatabaseAdapterFailureAtConnect;
            }

            if (errorCode == ErrorCode.Ok)
            {
                errorCode = RunImplementation(databaseAdapter);
                databaseAdapter.Disconnect();
            }

            if (errorCode == ErrorCode.Ok)
            {
                log.InfoFormat(CultureInfo.InvariantCulture, "{0} : succeeded", this);
            }
            else
            {
                log.ErrorFormat(CultureInfo.InvariantCulture, "{0} : FAILED.", this);
            }

            return errorCode;
        }

        public override string ToString()
        {
            if (CurrentVersion == null && NewVersion == null)
            {
                return Name;
            }

            return String.Format(CultureInfo.InvariantCulture, "Script \"{0}\": Increment database {1} from {2} to {3}",
                Name,
                DatabaseName,
                (CurrentVersion == null) ? "current" : CurrentVersion.ToString(),
                (NewVersion == null) ? "same" : NewVersion.ToString());
        }

        #endregion

        #region Non-public methods

        private static readonly ILog log = LogManager.GetLogger(typeof(Script));

        /// <summary>
        /// Runs the script against the database
        /// </summary>
        /// <param name="databaseAdapter">The database adapter.</param>
        /// <returns></returns>
        private ErrorCode RunImplementation(DatabaseAdapter databaseAdapter)
        {
            var errorCode = ErrorCode.Ok;

            if (WrapInTransaction)
            {
                if (!databaseAdapter.BeginTransaction())
                {
                    log.Error("Failed to begin the transaction on the database.");
                    return ErrorCode.DatabaseAdapterFailureAtBeginTransaction;
                }
            }

            if (null != CurrentVersion)
            {
                bool versionConfirmed;
                if (!databaseAdapter.ConfirmVersion(DatabaseName, CurrentVersion, out versionConfirmed))
                {
                    log.Error("Failed to check the current version of the database.");
                    return ErrorCode.DatabaseAdapterFailureAtConfirmVersion;
                }

                if (versionConfirmed)
                {
                    log.Info("The current version of the database is compatible with the script");
                }
                else
                {
                    log.Error("The current version of the database is not compatible with the script.");
                    return ErrorCode.IncorrectCurrentVersion;
                }
            };

            if (!databaseAdapter.RunCommand(DatabaseName, Command))
            {
                log.Error("Failed to execute the command on the database.");
                errorCode = ErrorCode.DatabaseAdapterFailureAtRunCommand;
            }

            if ((errorCode == ErrorCode.Ok) && (NewVersion != null))
            {
                if (!databaseAdapter.SetVersion(DatabaseName, NewVersion))
                {
                    log.Error("Failed to set the new version on the database.");
                    errorCode = ErrorCode.DatabaseAdapterFailureAtSetVersion;
                }
            }

            if ((errorCode == ErrorCode.Ok) && WrapInTransaction)
            {
                if (!databaseAdapter.CommitTransaction())
                {
                    log.Error("Failed to commit the transaction on the database.");
                    errorCode = ErrorCode.DatabaseAdapterFailureAtCommitTransaction;
                }
            }

            if (errorCode != ErrorCode.Ok)
            {
                if (WrapInTransaction)
                {
                    log.Warn("Rolling back the script transaction.");

                    if (!databaseAdapter.RollBackTransaction())
                    {
                        log.Error("An error occurred when rolling back the script transaction. You must check the database is in the correct state.");
                        errorCode = ErrorCode.DatabaseAdapterFailureAtRollbackTransaction;
                    }

                    log.Warn("The script transaction rolled back successfully");
                }
                else
                {
                    log.Warn("The script will not be rolled back. You must check the database is in the correct state.");
                }
            }

            return errorCode;
        }

        #endregion

    }
}
