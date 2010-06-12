//DatabaseScripter  Copyright (C) 2010  Philip Wood
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
