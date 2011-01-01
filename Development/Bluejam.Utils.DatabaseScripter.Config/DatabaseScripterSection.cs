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
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

using log4net;

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public class DatabaseScripterSection : IConfigurationSectionHandler
    {


        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            var configuration = new Domain.Values.Configuration();

            var configurationValidator = new ConfigurationValidator();
            var result = configurationValidator.Validate(section);
            if (result != Domain.ErrorCode.Ok)
            {
                log.Error("The configuration contains errors.");
                throw new Domain.DatabaseScripterException(result, "The configuration is invalid.");
            }

            // Gets the child element names and attributes.
            foreach (XmlNode child in section.ChildNodes)
            {
                switch (child.Name)
                {
                    case "environmentConfigurationPaths":
                        configuration.EnvironmentConfigurations.AddRange(GetEnvironmentConfigurations(child));
                        break;
                    case "manifestPath":
                        var manifestFactoryResult = ManifestFactory.Create(child.InnerText);
                        if (manifestFactoryResult.ErrorCode != Domain.ErrorCode.Ok)
                        {
                            throw new Domain.DatabaseScripterException(manifestFactoryResult.ErrorCode, "Failed to read manifest");
                        }
                        configuration.Manifest = manifestFactoryResult.Manifest;
                        break;
                }
            }


            return configuration;
        }

        #endregion

        #region Non-public 

        private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseScripterSection));

        private static Domain.Entities.EnvironmentConfigurationCollection GetEnvironmentConfigurations(XmlNode node)
        {
            var environmentConfigurations = new Domain.Entities.EnvironmentConfigurationCollection();

            foreach (XmlNode child in node.ChildNodes)
            {
                switch (child.Name)
                {                    
                    case "environmentConfigurationPath":
                        var result = EnvironmentConfigurationFactory.Create(child.InnerText);
                        if (result.ErrorCode != Domain.ErrorCode.Ok)
                        {
                            throw new Domain.DatabaseScripterException(result.ErrorCode, "Failed to read environment configuration");
                        }

                        environmentConfigurations.Add(result.EnvironmentConfiguration);
                        break;
                }
            }

            return environmentConfigurations;
        }

        #endregion

    }
}
