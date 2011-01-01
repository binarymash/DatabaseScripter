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
