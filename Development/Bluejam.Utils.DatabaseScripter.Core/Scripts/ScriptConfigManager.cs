using System.Configuration;
using System.Globalization;
using log4net;

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

            if (Config.DatabaseScripterConfig.Instance.GlobalScriptProperties.ContainsKey(propertyName))
            {
                return Config.DatabaseScripterConfig.Instance.GlobalScriptProperties[propertyName];
            }

            log.ErrorFormat(CultureInfo.InvariantCulture, "Could not find script key \"{0}\" property in the configuration", propertyName);
            throw new DatabaseScripterException(ErrorCode.CouldNotFindPropertyForScript, propertyName);
        }

        public static string GetConnectionString(Config.ScriptConfig scriptConfig)
        {
            return ConfigurationManager.ConnectionStrings[GetConfig(scriptConfig, "connection")].ConnectionString;
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(ScriptConfigManager));
    }
}
