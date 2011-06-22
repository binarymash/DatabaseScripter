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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public class ManifestValidator : SchemaValidatorBase, Interfaces.IManifestValidator
    {

        #region Non-public

        private static readonly ILog log = LogManager.GetLogger(typeof(ManifestValidator));
        private static object syncLock = new Object();
        private static bool isValid;

        #endregion

        public ManifestValidator() : base("Bluejam.Utils.DatabaseScripter.Config.ManifestSchema.xsd")
        {
        }

        public Interfaces.Result Validate(string manifestFilePath)
        {
            log.DebugFormat(CultureInfo.InvariantCulture, "Validating manifest file at {0}", manifestFilePath);
            lock (syncLock)
            {
                try
                {
                    isValid = true;
                    var assembly = Assembly.GetExecutingAssembly();
                    var execPath = new FileInfo(assembly.Location);
                    if (!Path.IsPathRooted(manifestFilePath))
                    {
                        manifestFilePath = Path.Combine(execPath.Directory.FullName, manifestFilePath);
                        log.DebugFormat(CultureInfo.InvariantCulture, "The manifest path is relative; expanded to {0}", manifestFilePath);
                    }

                    if (!File.Exists(manifestFilePath))
                    {
                        log.ErrorFormat(CultureInfo.InvariantCulture, "The manifest file could not be found at {0}", manifestFilePath);
                        return new Interfaces.Result(Domain.Interfaces.ErrorCode.CouldNotFindManifest);
                    }

                    var xmlReaderSettings = new XmlReaderSettings();
                    xmlReaderSettings.Schemas.Add(Schema);
                    xmlReaderSettings.ValidationType = ValidationType.Schema;
                    xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                    var manifestReader = XmlReader.Create(manifestFilePath, xmlReaderSettings);
                    while (manifestReader.Read()) ;

                    if (isValid)
                    {
                        log.Debug("The manifest is valid");
                    }
                    else
                    {
                        log.Error("The manifest is invalid");
                    }
                }
                catch (XmlException ex)
                {
                    log.Error("An error occurred when validating the manifest", ex);
                    isValid = false;
                }

                return new Interfaces.Result(isValid ? Domain.Interfaces.ErrorCode.Ok : Domain.Interfaces.ErrorCode.InvalidManifest);
            }
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //TODO: store errors in collection on this.
            isValid = false;
            
            log.ErrorFormat(CultureInfo.InvariantCulture, "The manifest contains an error: {0}", e.Message);
        }

    }
}
