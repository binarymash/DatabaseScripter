﻿//DatabaseScripter  Copyright (C) 2011  Philip Wood
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
    public class ConfigurationValidator : SchemaValidatorBase
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationValidator));
        private static object syncLock = new Object();
        private static bool isValid;

        public ConfigurationValidator() : base("Bluejam.Utils.DatabaseScripter.Config.ConfigSchema.xsd")
        {
        }

        public Domain.ErrorCode Validate(IXPathNavigable configSectionNode)
        {
            log.Debug("Validating the configuration");

            lock (syncLock)
            {
                try
                {
                    isValid = true;

                    //TODO: is there a better way to do this?
                    var navigator = configSectionNode.CreateNavigator();
                    var doc = new XmlDocument();
                    doc.LoadXml(navigator.OuterXml);
                    doc.Schemas.Add(Schema);
                    doc.Validate(ValidationCallback);

                    if (isValid)
                    {
                        log.Debug("The configuration is valid");
                    }
                    else
                    {
                        log.Error("The configuration is invalid");
                    }
                }
                catch (XmlException ex)
                {
                    log.Error("An error occurred when validating the configuration", ex);
                    isValid = false;
                }

                return isValid ? Domain.ErrorCode.Ok : Domain.ErrorCode.InvalidConfig;
            }
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //TODO: store errors in collection on this.
            isValid = false;
            log.ErrorFormat(CultureInfo.InvariantCulture, "The configuration contains an error: {0}", e.Message);
        }

    }
}
