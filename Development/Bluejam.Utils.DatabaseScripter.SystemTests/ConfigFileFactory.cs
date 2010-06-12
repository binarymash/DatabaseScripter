using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Bluejam.Utils.DatabaseScripter.SystemTests
{
    /// <summary>
    /// Creates config files for test cases
    /// </summary>
    class ConfigFileFactory
    {

        /// <summary>
        /// Creates a config file at the specified path using the specified manifest resource, and modifies it with the
        /// specified user config
        /// </summary>
        /// <param name="configFilePath">The config file path.</param>
        /// <param name="manifestResourceName">The name of the manifest resource.</param>
        public static void SetUpConfig(string configFilePath, string manifestResourceName, List<KeyValuePair<string, string>> userConfig)
        {
            if (File.Exists(configFilePath))
            {
                File.Delete(configFilePath);
            }

            var xmlDoc = new XmlDocument();
            var assembly = Assembly.GetExecutingAssembly();
            xmlDoc.Load(assembly.GetManifestResourceStream(manifestResourceName));

            if (userConfig != null)
            {
                foreach (var item in userConfig)
                {
                    var node = xmlDoc.SelectSingleNode(item.Key);
                    if (node == null)
                    {
                        throw new ArgumentException("Xpath not found", item.Key);
                    }
                    node.Value = item.Value;
                }
            }

            using (var configFileStream = new FileStream(configFilePath, FileMode.Create))
            {
                xmlDoc.Save(configFileStream);
            }
        }

        /// <summary>
        /// Creates a config file at the specified path using the specified manifest resource.
        /// </summary>
        /// <param name="configFilePath">The config file path.</param>
        /// <param name="manifestResourceName">Name of the manifest resource.</param>
        public static void SetUpConfig(string configFilePath, string manifestResourceName)
        {
            SetUpConfig(configFilePath, manifestResourceName, new List<KeyValuePair<string, string>>());
        }

    }
}
