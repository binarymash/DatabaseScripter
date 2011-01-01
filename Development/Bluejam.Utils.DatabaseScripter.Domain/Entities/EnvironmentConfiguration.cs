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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using NHibernate.Validator.Constraints;

using SharpArch.Core.DomainModel;

using log4net;

namespace Bluejam.Utils.DatabaseScripter.Domain.Entities
{
    [Serializable]
    [XmlRoot(Namespace = "http://code.google.com/p/databasescripter/2010/08/29/EnvironmentConfigurationSchema")]
    public class EnvironmentConfiguration : Entity
    {

        //private static readonly ILog log = LogManager.GetLogger(typeof(EnvironmentConfiguration));

        #region Properties

        /// <summary>
        /// Gets or sets the name of the environment.
        /// </summary>
        /// <value>The name of the environment.</value>
        [DomainSignature]
        [Validators.NotNullNotWhiteSpace(Message="A name must be specified for an environment configuration")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the environment's collection of global properties.
        /// </summary>
        /// <value>The properties.</value>
        [Valid]
        [Validators.UniqueMembers(Message = "There is more than one property with the same name")]
        public PropertyCollection Properties { get; set; }

        /// <summary>
        /// Gets or sets the environment's collection of script configurations.
        /// </summary>
        /// <value>The script configurations.</value>
        [Valid]
        [Validators.UniqueMembers(Message = "There is more than one script configuration with the same name")]
        public ScriptConfigurationCollection ScriptConfigurations { get; set; }

        #endregion

        #region Public methods

        public ScriptConfiguration GetFlatConfigurationForScript(string scriptName)
        {
            if (scriptName == null)
            {
                throw new ArgumentNullException("scriptName");
            }

            var scriptConfiguration = new ScriptConfiguration(scriptName);

            var scriptSpecificConfiguration = ScriptConfigurations.Find(item => item.Name == scriptName);
            if (scriptSpecificConfiguration != null)
            {
                scriptConfiguration.Properties.AddRange(scriptSpecificConfiguration.Properties);
            }
            foreach (var property in this.Properties)
            {
                if (!scriptConfiguration.Properties.Exists(item => item.Name == property.Name))
                {
                    scriptConfiguration.Properties.Add(property);
                }
            }

            return scriptConfiguration;
        }

        #endregion

        #region Constructors

        public EnvironmentConfiguration()
        {
            Properties = new PropertyCollection();
            ScriptConfigurations = new ScriptConfigurationCollection();
        }

        #endregion

    }
}
