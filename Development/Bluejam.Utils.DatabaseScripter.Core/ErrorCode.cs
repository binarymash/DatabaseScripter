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
        InvalidConfig = 7
    }
}
