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
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Domain
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
