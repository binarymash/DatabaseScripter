using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{

    public sealed class Configuration
    {

        #region Non-public

        private static volatile Configuration _instance;
        private static object _syncRoot = new Object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the global script properties.
        /// </summary>
        /// <value>The global script properties.</value>
        public Dictionary<string, string> GlobalScriptProperties { get; set; }

        /// <summary>
        /// Gets or sets the scripts.
        /// </summary>
        /// <value>The scripts.</value>
        public List<ScriptConfig> Scripts { get; set; }

        /// <summary>
        /// Gets or sets the manifest file.
        /// </summary>
        /// <value>The file path.</value>
        public Manifest.Manifest Manifest { get; set; }

        #endregion

        #region Singleton implementation

        private Configuration()
        {
            Scripts = new List<ScriptConfig>();
            GlobalScriptProperties = new Dictionary<string, string>();
            Manifest = new Manifest.Manifest();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new Configuration();
                    }
                }

                return _instance;
            }
        }

        #endregion

    }

}
