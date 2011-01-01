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
using System.Text;
using System.Xml.Serialization;

using SharpArch.Core.DomainModel;

namespace Bluejam.Utils.DatabaseScripter.Domain.Entities
{
    [Serializable]
    public class ScriptManifest : Entity
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")]
        [DomainSignature]
        [Validators.NotNullNotWhiteSpace(Message="A script manifest must have a name")]
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
        [Validators.NotNullNotWhiteSpace(Message = "A script manifest must have a path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the script should be wrapped in a transaction.
        /// </summary>
        /// <value><c>true</c> if the script should be wrapped in a transaction; otherwise, <c>false</c>.</value>
        [XmlAttribute("transactional")]
        public bool WrapInTransaction { get; set; }

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
