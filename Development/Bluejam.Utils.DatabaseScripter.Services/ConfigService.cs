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

using Config = Bluejam.Utils.DatabaseScripter.Config;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Services
{
    public class ConfigService
    {

        private Config.ConfigurationFactory configurationFactory = new Config.ConfigurationFactory();
        private Config.ExecutionPlanFactory executionPlanFactory = new Config.ExecutionPlanFactory();
        private Config.ManifestValidator manifestValidator = new Config.ManifestValidator();
        private Config.ConfigurationValidator configValidator = new Config.ConfigurationValidator();
        private Config.EnvironmentConfigurationValidator environmentConfigValidator = new Config.EnvironmentConfigurationValidator();

        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigService));

        public Config.ConfigurationFactory ConfigurationFactory { get; set; }
        public Config.ExecutionPlanFactory ExecutionPlanFactory { get; set; }
        public Config.ManifestValidator ManifestValidator { get; set; }
        public Config.ConfigurationValidator ConfigValidator { get; set; }
        public Config.EnvironmentConfigurationValidator EnvironmentConfigValidator { get; set; }

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

        public ConfigurationResult GetConfiguration(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            } 
            
            var errorCode = Domain.ErrorCode.Ok;
            Domain.Values.Configuration configuration = null;

            try
            {
                configuration = configurationFactory.Create(args);
            }
            catch (Domain.DatabaseScripterException ex)
            {
                log.Error("An error occurred. Check the debug information that follows.", ex);
                errorCode = ex.ErrorCode;
            }

            return new ConfigurationResult(errorCode, configuration);
        }

        public ExecutionPlanResult GetExecutionPlan(Domain.Values.Configuration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var errorCode = Domain.ErrorCode.Ok;
            Domain.Values.ExecutionPlan executionPlan = null;

            try
            {
                executionPlan = executionPlanFactory.Create(configuration);
            }
            catch (Domain.DatabaseScripterException ex)
            {
                log.Error("An error occurred. Check the debug information that follows.", ex);
                errorCode = ex.ErrorCode;
            }

            return new ExecutionPlanResult(errorCode, executionPlan);
        }

    }
}
