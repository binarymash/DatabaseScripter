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
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{

    public sealed class DatabaseScripterConfig
    {

        #region Non-public

        private static volatile DatabaseScripterConfig _instance;
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
        public Manifest Manifest { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the scripter will run in preview mode.
        /// </summary>
        /// <value><c>true</c> if running in preview mode; otherwise, <c>false</c>.</value>
        public bool Preview { get; set; }

        #endregion

        #region Singleton implementation

        private DatabaseScripterConfig()
        {
            Scripts = new List<ScriptConfig>();
            GlobalScriptProperties = new Dictionary<string, string>();
            Manifest = new Manifest();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static DatabaseScripterConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new DatabaseScripterConfig();
                    }
                }

                return _instance;
            }
        }

        #endregion

    }

}
