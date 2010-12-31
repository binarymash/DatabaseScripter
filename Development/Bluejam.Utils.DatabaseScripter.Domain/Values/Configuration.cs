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
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Validator.Constraints;

namespace Bluejam.Utils.DatabaseScripter.Domain.Values
{

    public sealed class Configuration : Core.DomainModel.ValidatableValueObject
    {

        #region Constructors

        public Configuration()
        {
            ConnectionStrings = new ConnectionStringSettingsCollection();
            EnvironmentConfigurations = new Entities.EnvironmentConfigurationCollection();
            NameOfScriptsToRun = new List<string>();
        }

        #endregion

        #region Properties

        [Valid]
        [NotEmpty(Message = "There are no connection strings")]
        public ConnectionStringSettingsCollection ConnectionStrings { get; set; }

        /// <summary>
        /// Gets or sets the environment configurations.
        /// </summary>
        /// <value>The environment configurations.</value>
        [Valid]
        [NotEmpty(Message = "There are no environment configurations")]
        [Validators.UniqueMembers(Message = "There is more than one environment configuration with the same name")]
        public Entities.EnvironmentConfigurationCollection EnvironmentConfigurations { get; private set; }

        /// <summary>
        /// Gets or sets the manifest file.
        /// </summary>
        /// <value>The file path.</value>
        [Valid]
        public Values.Manifest Manifest { get; set; }

        public Domain.Values.Version CurrentVersion { get; set; }
        
        public Domain.Values.Version TargetVersion { get; set; }

        public bool Pause { get; set; }

        public bool ManifestSchema { get; set; }

        public bool EnvironmentConfigSchema { get; set; }

        public bool ConfigSchema { get; set; }

        public string Environment { get; set; }

        public List<string> NameOfScriptsToRun { get; private set; }

        public bool Preview { get; set; }

        #endregion

    }

}
