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

using Bluejam.Utils.DatabaseScripter.Config.Interfaces;

using log4net;

namespace Bluejam.Utils.DatabaseScripter.Services
{
    public class ConfigService : Interfaces.IConfigService
    {

        public ConfigService(IConfigurationFactory configurationFactory, IManifestValidator manifestValidator, IConfigurationValidator configValidator, IEnvironmentConfigurationValidator environmentConfigValidator)
        {
            this.configurationFactory = configurationFactory;
            this.manifestValidator = manifestValidator;
            this.configValidator = configValidator;
            this.environmentConfigValidator = environmentConfigValidator;
        }

        private IConfigurationFactory configurationFactory;
        private IManifestValidator manifestValidator;
        private IConfigurationValidator configValidator;
        private IEnvironmentConfigurationValidator environmentConfigValidator;

        //TODO: windsor
        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigService));

        public string ManifestSchema
        {
            get { return manifestValidator.SchemaString; }
        }

        public string ConfigSchema
        {
            get { return configValidator.SchemaString; }
        }

        public string EnvironmentConfigSchema
        {
            get { return environmentConfigValidator.SchemaString; }
        }

        public Interfaces.IConfigurationResult GetConfiguration(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            } 
            
            var errorCode = Domain.Interfaces.ErrorCode.Ok;
            Domain.Values.Configuration configuration = null;

            try
            {
                configuration = configurationFactory.Create(args);
            }
            catch (Domain.Interfaces.DatabaseScripterException ex)
            {
                log.Error("An error occurred. Check the debug information that follows.", ex);
                errorCode = ex.ErrorCode;
            }

            return new ConfigurationResult(errorCode, configuration);
        }

    }
}
