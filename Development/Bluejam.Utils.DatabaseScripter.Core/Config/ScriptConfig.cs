using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class ScriptConfig
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>The properties.</value>
        public Dictionary<string, string> Properties { get; set; }

        #endregion

        #region Constructor

        public ScriptConfig()
        {
            Properties = new Dictionary<string, string>();
        }

        #endregion

    }
}
