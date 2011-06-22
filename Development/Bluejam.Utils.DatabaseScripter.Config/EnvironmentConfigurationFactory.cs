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

using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Config
{

    public static class EnvironmentConfigurationFactory
    {

        #region Public methods

        /// <summary>
        /// Creates the environment configuration object from a file at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static EnvironmentConfigurationFactoryResult Create(string path)
        {
            log.DebugFormat(CultureInfo.InvariantCulture, "Reading environment configuration at {0}", path);

            try
            {
                var execPath = new FileInfo(Assembly.GetExecutingAssembly().Location);
                if (!Path.IsPathRooted(path))
                {
                    path = Path.Combine(execPath.Directory.FullName, path);
                    log.DebugFormat(CultureInfo.InvariantCulture, "The environment configuration path is relative; expanded to {0}", path);
                }

                if (!File.Exists(path))
                {
                    log.ErrorFormat(CultureInfo.InvariantCulture, "The environment configuration file could not be found at {0}", path);
                    return new EnvironmentConfigurationFactoryResult(Domain.Interfaces.ErrorCode.CouldNotFindEnvironmentConfiguration, null);
                }

                var environmentConfigurationValidator = new EnvironmentConfigurationValidator();
                var result = environmentConfigurationValidator.Validate(path);
                if (result.ErrorCode != Domain.Interfaces.ErrorCode.Ok)
                {
                    log.Error("The environment configuration file is invalid");
                    return new EnvironmentConfigurationFactoryResult(Domain.Interfaces.ErrorCode.InvalidEnvironmentConfiguration, null);
                }

                //deserialize
                var xmlReader = XmlReader.Create(path);
                var xmlSerializer = new XmlSerializer(typeof(Domain.Entities.EnvironmentConfiguration));
                var environmentConfiguration = (Domain.Entities.EnvironmentConfiguration)xmlSerializer.Deserialize(xmlReader);

                return new EnvironmentConfigurationFactoryResult(Domain.Interfaces.ErrorCode.Ok, environmentConfiguration);
            }
            catch (Domain.Interfaces.DatabaseScripterException ex)
            {
                log.Error("An unexpected error occurred when reading the environment configuration file.", ex);
                return new EnvironmentConfigurationFactoryResult(Domain.Interfaces.ErrorCode.UnknownError, null);
            }
        }

        #endregion

        #region Non-public

        private static readonly ILog log = LogManager.GetLogger(typeof(EnvironmentConfigurationFactory));

        #endregion


    }
}
