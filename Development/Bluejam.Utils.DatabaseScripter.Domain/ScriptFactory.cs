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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using log4net;


namespace Bluejam.Utils.DatabaseScripter.Domain
{
    public static class ScriptFactory
    {

        #region Public methods

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static ICollection<Script> Create(Configuration configuration, ExecutionPlan executionPlan, Domain.ConfigInjector configInjector)
        {
            var scripts = new List<Script>();

            foreach (var scriptName in executionPlan.NameOfScriptsToRun)
            {
                var scriptManifest = configuration.Manifest.GetManifest(scriptName);
                if (scriptManifest == null)
                {
                    //TODO: add errors to scripts
                    var message = string.Format(CultureInfo.InvariantCulture, "Could not find script \"{0}\" in the manifest file.", scriptName);
                    log.Error(message);
                    throw new DatabaseScripterException(ErrorCode.CouldNotFindScriptInManifest, message);
                }

                var environmentConfiguration = configuration.EnvironmentConfigurations.Find(executionPlan.Environment);
                if (environmentConfiguration == null)
                {
                    //TODO: add errors to scripts
                    var message = string.Format(CultureInfo.InvariantCulture, "Could not find environment configuration \"{0}\"", executionPlan.Environment);
                    log.Error(message);
                    throw new DatabaseScripterException(ErrorCode.CouldNotFindEnvironmentConfiguration, message);
                }

                var flatScriptConfiguration = environmentConfiguration.GetFlatConfigurationForScript(scriptName);
                var connectionString = configuration.ConnectionStrings[flatScriptConfiguration.Properties.Find("connection").Value].ConnectionString;

                scripts.Add(CreateScript(flatScriptConfiguration, scriptManifest, configuration, configInjector, connectionString));
            }

            return scripts;
        }

        #endregion

        #region Non-public methods

        private static readonly ILog log = LogManager.GetLogger(typeof(ScriptFactory));

        private static Domain.Script CreateScript(

            Domain.ScriptConfiguration scriptConfiguration,
            Domain.ScriptManifest scriptManifest, 
            Domain.Configuration configuration, 
            Domain.ConfigInjector configInjector,
            string connectionString)
        {
            var scriptFileName = Path.Combine(Path.GetDirectoryName(configuration.Manifest.FilePath), scriptManifest.Path);
            if (!File.Exists(scriptFileName))
            {
                throw new DatabaseScripterException(ErrorCode.ScriptFileDoesNotExist, scriptFileName);
            }

            var command = File.ReadAllText(scriptFileName);

            return new Domain.Script(scriptManifest.Name,
                scriptManifest.Description,
                scriptConfiguration.Properties.Find("databaseName").Value,
                connectionString, 
                scriptManifest.WrapInTransaction,
                (scriptManifest.CurrentVersion == null) ? null : new Domain.Version(scriptManifest.CurrentVersion),
                (scriptManifest.NewVersion == null) ? null : new Domain.Version(scriptManifest.NewVersion),
                configInjector.InjectConfig(command, scriptConfiguration.Properties));
        }

        #endregion

    }
}
