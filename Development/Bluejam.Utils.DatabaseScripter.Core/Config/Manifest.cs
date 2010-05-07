using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    [Serializable]
    [XmlRoot(Namespace = "http://code.google.com/p/databasescripter/2010/04/25/ManifestSchema")]
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
        public List<ScriptManifest> ScriptManifests { get; set; }

        /// <summary>
        /// Gets the manifest.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        /// <returns></returns>
        public ScriptManifest GetManifest(string name)
        {
            return ScriptManifests.ToList().Find(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

    }
}
