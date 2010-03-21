using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    [Serializable]
    public class Manifest
    {

        #region Properties

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        [XmlIgnore]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the scripts.
        /// </summary>
        /// <value>The scripts.</value>
        public List<ScriptManifest> UnversionedScripts { get; set; }

        /// <summary>
        /// Gets or sets the versioned scripts.
        /// </summary>
        /// <value>The versioned scripts.</value>
        public List<VersionedScriptManifest> VersionedScripts { get; set; }

        /// <summary>
        /// Gets the manifest.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ScriptManifest GetManifest(string name)
        {
            var script = UnversionedScripts.ToList().Find(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
            if (script == null)
            {
                script = VersionedScripts.ToList().Find(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
            }
            return script;
        }

        #endregion

    }
}
