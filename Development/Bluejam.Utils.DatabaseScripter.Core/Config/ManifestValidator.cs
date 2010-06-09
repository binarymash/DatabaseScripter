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

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class ManifestValidator : SchemaValidatorBase
    {

        #region Non-public

        private static readonly ILog log = LogManager.GetLogger(typeof(ManifestValidator));
        private static object syncLock = new Object();
        private static bool isValid;

        #endregion

        public ManifestValidator() : base("Bluejam.Utils.DatabaseScripter.Core.ManifestSchema.xsd")
        {
        }

        public Result Validate(string manifestFilePath)
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
                        return new Result(ErrorCode.CouldNotFindManifest);
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

                return new Result(isValid ? ErrorCode.Ok : ErrorCode.InvalidManifest);
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
