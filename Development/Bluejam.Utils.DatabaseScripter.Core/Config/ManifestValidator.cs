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

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public class ManifestValidator : SchemaValidatorBase
    {
        private static object syncLock = new Object();
        private static bool isValid;

        public ManifestValidator() : base("Bluejam.Utils.DatabaseScripter.Core.ManifestSchema.xsd")
        {
        }

        public bool IsValid(string manifestFilePath)
        {
            lock (syncLock)
            {
                try
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    isValid = true;
                    var execPath = new FileInfo(assembly.Location);
                    if (!Path.IsPathRooted(manifestFilePath))
                    {
                        manifestFilePath = Path.Combine(execPath.Directory.FullName, manifestFilePath);
                    }

                    if (!File.Exists(manifestFilePath))
                    {
                        //TODO: log missing manifest path
                        throw new DatabaseScripterException(ErrorCode.CouldNotFindManifest);
                    }

                    var xmlReaderSettings = new XmlReaderSettings();
                    xmlReaderSettings.Schemas.Add(Schema);
                    xmlReaderSettings.ValidationType = ValidationType.Schema;
                    xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                    var manifestReader = XmlReader.Create(manifestFilePath, xmlReaderSettings);
                    while (manifestReader.Read()) ;

                    return isValid;
                }
                catch (XmlException ex)
                {
                    //TODO: log xml exception
                    throw new DatabaseScripterException(ErrorCode.InvalidManifest, "An exception occurred when validating the manifest schema.", ex);
                }
            }
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //TODO: log validation errors
            //TODO: store errors in collection on this.
            isValid = false;
            Console.WriteLine("Validation Error: {0}", e.Message);
        }

    }
}
