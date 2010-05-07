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
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(execPath.Directory.FullName, path);
            } 
            
            if (!File.Exists(path))
            {
                //TODO: log missing manifest
                return null;
            }

            if (!ManifestValidator.IsValid(path))
            {
                //TODO: log invalid schema
                return null;
            }

            //deserialize
            var xmlReader = XmlReader.Create(path);
            var xmlSerializer = new XmlSerializer(typeof(Manifest));
            var manifest = (Manifest)xmlSerializer.Deserialize(xmlReader);
            manifest.FilePath = path;

            return manifest;
        }

    }
}
