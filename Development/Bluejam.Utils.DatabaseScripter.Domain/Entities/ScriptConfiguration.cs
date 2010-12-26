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
using System.Xml.Serialization;

using SharpArch.Core.DomainModel;

using NHibernate.Validator.Constraints;

namespace Bluejam.Utils.DatabaseScripter.Domain.Entities
{
    [Serializable]
    public class ScriptConfiguration : Entity
    {

        #region Properties

        /// <summary>
        /// Gets or sets the name of the script to which this configuration relates.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")]
        [DomainSignature]
        [Validators.NotNullNotWhiteSpace(Message="A name must be supplied for the script configuration")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the script's properties.
        /// </summary>
        /// <value>The properties.</value>
        [Valid]
        [NotNull(Message = "A collection of properties must be specified for the script configuration")]
        [Validators.UniqueMembers(Message = "There is more than one property with the same name")]
        public PropertyCollection Properties { get; set; }

        #endregion

        #region Constructors

        public ScriptConfiguration()
        {
            Properties = new PropertyCollection();
        }

        public ScriptConfiguration(string name) : this()
        {
            Name = name;
        }

        #endregion

    }
}
