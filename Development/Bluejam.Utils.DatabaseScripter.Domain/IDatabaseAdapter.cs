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
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Domain
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
