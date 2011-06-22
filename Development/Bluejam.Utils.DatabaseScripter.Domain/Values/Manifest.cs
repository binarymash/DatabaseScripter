//DatabaseScripter  Copyright (C) 2011  Philip Wood
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

namespace Bluejam.Utils.DatabaseScripter.Domain.Values
{
    [Serializable]
    [XmlRoot(Namespace = "http://code.google.com/p/databasescripter/2010/04/25/ManifestSchema")]
    public class Manifest : Core.DomainModel.ValidatableValueObject, Interfaces.IManifest
    {

        #region Non-public

        Interfaces.IVersionFactory versionFactory;

        #endregion

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
        public List<Interfaces.IScriptManifest> ScriptManifests { get; set; }

        /// <summary>
        /// Gets the manifest.
        /// </summary>
        /// <param name="name">The name of the script.</param>
        /// <returns></returns>
        public Interfaces.IScriptManifest GetManifest(string name)
        {
            return ScriptManifests.Find(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public Interfaces.IVersion LatestVersion
        {
            get
            {
                Values.Version latestVersion = null;
                foreach (var scriptManifest in ScriptManifests)
                {
                    if (!string.IsNullOrEmpty(scriptManifest.NewVersion))
                    {
                        var version = new Values.Version(scriptManifest.NewVersion);
                        if ((latestVersion == null) || (version > latestVersion))
                        {
                            latestVersion = version;
                        }
                    }
                }
                return latestVersion;
            }
        }

        public List<Interfaces.IScriptManifest> GetConcurrentScripts(Interfaces.IVersion startVersion, Interfaces.IVersion endVersion)
        {            
            if (startVersion == null)
            {
                throw new ArgumentNullException("startVersion");
            }

            if (endVersion == null)
            {
                throw new ArgumentNullException("endVersion");
            }

            if (endVersion < startVersion)
            {
                throw new ArgumentException("endVersion must be greater than startVersion", "endVersion");
            }

            var concurrentScriptManifests = new List<Domain.Entities.ScriptManifest>();

            var versionedScriptManifests = ScriptManifests.FindAll(item => item.CurrentVersion != null && item.NewVersion != null);
            var scriptManifest = versionedScriptManifests.Find(item => new Domain.Values.Version(item.CurrentVersion) == startVersion);
            while (scriptManifest != null)
            {
                concurrentScriptManifests.Add(scriptManifest);
                if (new Domain.Values.Version(scriptManifest.NewVersion) == endVersion)
                {
                    return concurrentScriptManifests;
                }
                scriptManifest = versionedScriptManifests.Find(item => new Domain.Values.Version(item.CurrentVersion) == new Domain.Values.Version(scriptManifest.NewVersion));
            }

            return new List<Interfaces.IScriptManifest>();
        }

        #endregion

        #region Constructors

        public Manifest(Interfaces.IVersionFactory versionFactory)
        {
            this.versionFactory = versionFactory;
        }

        #endregion
    }
}
