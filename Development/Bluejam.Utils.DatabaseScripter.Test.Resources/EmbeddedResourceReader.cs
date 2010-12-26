using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources
{
    public abstract class EmbeddedResourceReader
    {

        public static string GetResourceString(string fileName, string resourceName)
        {
            var assembly = Assembly.LoadFile(fileName);
            using (var schemaResourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                var reader = new StreamReader(schemaResourceStream);
                return reader.ReadToEnd();
            }
        }

        public static string ConfigSchema
        {
            get { return GetResourceString(Path.Combine(System.Environment.CurrentDirectory, "Bluejam.Utils.DatabaseScripter.Config.dll"), "Bluejam.Utils.DatabaseScripter.Config.ConfigSchema.xsd"); }
        }

        public static string EnvironmentConfigSchema
        {
            get { return GetResourceString(Path.Combine(System.Environment.CurrentDirectory, "Bluejam.Utils.DatabaseScripter.Config.dll"), "Bluejam.Utils.DatabaseScripter.Config.EnvironmentConfigurationSchema.xsd"); }
        }

        public static string ManifestSchema
        {
            get { return GetResourceString(Path.Combine(System.Environment.CurrentDirectory, "Bluejam.Utils.DatabaseScripter.Config.dll"), "Bluejam.Utils.DatabaseScripter.Config.ManifestSchema.xsd"); }
        }
    }
}
