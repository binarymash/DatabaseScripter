using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    [Serializable]
    public class ScriptManifest
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        [XmlAttribute("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the script should be wrapped in a transaction.
        /// </summary>
        /// <value><c>true</c> if the script should NOT be wrapped in a transaction; otherwise, <c>false</c>.</value>
        [XmlAttribute("transaction")]
        public bool WrapInTransaction { get; set; }

        #endregion

    }
}
