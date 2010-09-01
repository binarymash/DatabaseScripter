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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public class EnvironmentConfigurationValidator : SchemaValidatorBase
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationValidator));
        private static object syncLock = new Object();
        private static bool isValid;

        public EnvironmentConfigurationValidator() : base("Bluejam.Utils.DatabaseScripter.Config.EnvironmentConfigurationSchema.xsd")
        {
        }

        public Result Validate(string environmentConfigurationFilePath)
        {
            log.Debug(string.Format(CultureInfo.InvariantCulture, "Validating the environment configuration {0}", environmentConfigurationFilePath));
            lock (syncLock)
            {
                try
                {
                    isValid = true;
                    var assembly = Assembly.GetExecutingAssembly();
                    var execPath = new FileInfo(assembly.Location);
                    if (!Path.IsPathRooted(environmentConfigurationFilePath))
                    {
                        environmentConfigurationFilePath = Path.Combine(execPath.Directory.FullName, environmentConfigurationFilePath);
                        log.DebugFormat(CultureInfo.InvariantCulture, "The environment configuration path is relative; expanded to {0}", environmentConfigurationFilePath);
                    }

                    if (!File.Exists(environmentConfigurationFilePath))
                    {
                        log.ErrorFormat(CultureInfo.InvariantCulture, "The environment configuration file could not be found at {0}", environmentConfigurationFilePath);
                        return new Result(Domain.ErrorCode.CouldNotFindEnvironmentConfiguration);
                    }

                    var xmlReaderSettings = new XmlReaderSettings();
                    xmlReaderSettings.Schemas.Add(Schema);
                    xmlReaderSettings.ValidationType = ValidationType.Schema;
                    xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                    var environmentConfigurationReader = XmlReader.Create(environmentConfigurationFilePath, xmlReaderSettings);
                    while (environmentConfigurationReader.Read()) ;

                    if (isValid)
                    {
                        log.Debug("The environment configuration is valid");
                    }
                    else
                    {
                        log.Error("The environment configuration is invalid");
                    }

                }
                catch (XmlException ex)
                {
                    log.Error("An error occurred when validating the environment configuration", ex);
                    isValid = false;
                }

                return new Result(isValid ? Domain.ErrorCode.Ok : Domain.ErrorCode.InvalidEnvironmentConfiguration);
            }
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //TODO: store errors in collection on this.
            isValid = false;
            log.ErrorFormat(CultureInfo.InvariantCulture, "The environment configuration contains an error: {0}", e.Message);
        }

    }
}
