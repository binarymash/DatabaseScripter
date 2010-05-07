using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public static class ManifestFactory
    {

        /// <summary>
        /// Creates the manifest object from a file at the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static Manifest Create(string path)
        {

            var execPath = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var schemaPath = Path.Combine(execPath.DirectoryName, "ManifestSchema.xsd");

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(execPath.Directory.FullName, path);
            }

            if (!File.Exists(path))
            {
                //TODO: log missing manifest
                return null;
            }

            //full validation
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var schemaStream = new StreamReader(Path.Combine(execPath.Directory.FullName, "ManifestSchema.xsd"));
                var schemaReader = XmlReader.Create(schemaStream);
                var xmlSchema = XmlSchema.Read(schemaReader, ValidationCallback);

                var validatingReader = new XmlValidatingReader(stream, XmlNodeType.Document, null);
                validatingReader.Schemas.Add(xmlSchema);
                validatingReader.ValidationType = ValidationType.Schema;
                validatingReader.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                while (validatingReader.Read()) ;
            }

            //deserialize
            var xmlReader = XmlReader.Create(path);
            var xmlSerializer = new XmlSerializer(typeof(Manifest));
            var manifest = (Manifest)xmlSerializer.Deserialize(xmlReader);
            manifest.FilePath = path;

            return manifest;
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //TODO: log validation errors
            Console.WriteLine("Validation Error: {0}", e.Message);
        }

    }
}
