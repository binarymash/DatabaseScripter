using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public static class ManifestValidator
    {
        static object syncLock = new Object();
        static bool isValid;

        public static bool IsValid(string manifestFilePath)
        {
            lock (syncLock)
            {
                try
                {
                    isValid = true;
                    var execPath = new FileInfo(Assembly.GetExecutingAssembly().Location);
                    if (!Path.IsPathRooted(manifestFilePath))
                    {
                        manifestFilePath = Path.Combine(execPath.Directory.FullName, manifestFilePath);
                    }

                    if (!File.Exists(manifestFilePath))
                    {
                        //TODO: log missing manifest path
                        throw new DatabaseScripterException(ErrorCode.CouldNotFindManifest);
                    }

                    var schemaPath = Path.Combine(execPath.DirectoryName, "ManifestSchema.xsd");
                    if (!File.Exists(schemaPath))
                    {
                        //TODO: log missing schema path
                        throw new DatabaseScripterException(ErrorCode.InvalidManifestSchema, "Could not find manifest schema");
                    }

                    XmlSchema xmlSchema = null;
                    using (var stream = new FileStream(schemaPath, FileMode.Open, FileAccess.Read))
                    {
                        var schemaStream = new StreamReader(schemaPath);
                        var schemaReader = XmlReader.Create(schemaStream);
                        xmlSchema = XmlSchema.Read(schemaReader, ValidationCallback);
                    }

                    var xmlReaderSettings = new XmlReaderSettings();
                    xmlReaderSettings.Schemas.Add(xmlSchema);
                    xmlReaderSettings.ValidationType = ValidationType.Schema;
                    xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                    var manifestReader = XmlReader.Create(manifestFilePath, xmlReaderSettings);
                    while (manifestReader.Read()) ;

                    return isValid;
                }
                catch (XmlException ex)
                {
                    //TODO: log xml exception
                    throw new DatabaseScripterException(ErrorCode.InvalidManifestSchema, "An exception occurred when validating the manifest schema.", ex);
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
