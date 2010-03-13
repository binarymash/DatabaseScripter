using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Core.Manifest
{
    [Serializable]
    public class VersionedScriptManifest : ScriptManifest
    {

        #region Properties

        /// <summary>
        /// Gets or sets the current version.
        /// </summary>
        /// <value>The current version.</value>
        public string CurrentVersion { get; set; }

        /// <summary>
        /// Gets or sets the new version.
        /// </summary>
        /// <value>The new version.</value>
        public string NewVersion { get; set; }

        #endregion

    }
}
