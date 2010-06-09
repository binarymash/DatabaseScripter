using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Core
{
    public enum ErrorCode
    {
        /// <summary>
        /// Ok
        /// </summary>
        Ok = 0,
        /// <summary>
        /// The manifest file could not be found
        /// </summary>
        CouldNotFindManifest = 1,
        /// <summary>
        /// A script could not be found in the manifest file
        /// </summary>
        CouldNotFindScript = 2,
            /// <summary>
        /// An exception occurred during script execution
        /// </summary>
        ScriptExecutionException = 3,
        /// <summary>
        /// The current version of the database schema is incorrect
        /// </summary>
        IncorrectCurrentVersion = 4,
        /// <summary>
        /// A script property could not be found
        /// </summary>
        CouldNotFindPropertyForScript = 5,
        /// <summary>
        /// The manifest does not validate against the manifest schema
        /// </summary>
        InvalidManifest = 6,
        /// <summary>
        /// The configuration contains errors
        /// </summary>
        InvalidConfig = 7,
        /// <summary>
        /// The preview adapter could not be created
        /// </summary>
        FailedToCreatePreviewAdapter = 8,
        /// <summary>
        /// The database adapter could not be created
        /// </summary>
        FailedToCreateDatabaseAdapter = 9,
        /// <summary>
        /// An unknown error occurred
        /// </summary>
        UnknownError = 10,



        /// <summary>
        /// The database adapter failed when connecting to the database
        /// </summary>
        DatabaseAdapterFailureAtConnect = 100,
        /// <summary>
        /// The database adapter failed when beginning the transaction
        /// </summary>
        DatabaseAdapterFailureAtBeginTransaction = 101,
        /// <summary>
        /// The database adapter failed when confirming the database version
        /// </summary>
        DatabaseAdapterFailureAtConfirmVersion = 102,
        /// <summary>
        /// The database adapter failed when executing the command
        /// </summary>
        DatabaseAdapterFailureAtRunCommand = 103,
        /// <summary>
        /// The database adapter failed when setting the schema version
        /// </summary>
        DatabaseAdapterFailureAtSetVersion = 104,
        /// <summary>
        /// The database adapter failed when committing the transaction
        /// </summary>
        DatabaseAdapterFailureAtCommitTransaction = 105,
        /// <summary>
        /// The database adapter failed when rolling back the transaction
        /// </summary>
        DatabaseAdapterFailureAtRollbackTransaction = 106,

    }
}
