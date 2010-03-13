using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace Bluejam.Utils.DatabaseScripter.Core.Scripts
{
    public static class ScriptConfigManager
    {
        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="scriptConfig">The script config.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static string GetConfig(Config.ScriptConfig scriptConfig, string propertyName)
        {
            if (scriptConfig.Properties.ContainsKey(propertyName))
            {
                return scriptConfig.Properties[propertyName];
            }

            return Config.Configuration.Instance.GlobalScriptProperties[propertyName];
        }

        public static string GetConnectionString(Config.ScriptConfig scriptConfig)
        {
            return ConfigurationManager.ConnectionStrings[GetConfig(scriptConfig, "connection")].ConnectionString;
        }
    }
}
