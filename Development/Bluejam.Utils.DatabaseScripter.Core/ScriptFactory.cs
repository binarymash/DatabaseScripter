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

using System.Collections.Generic;
using System.Globalization;
using System.IO;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

using log4net;


namespace Bluejam.Utils.DatabaseScripter.Core
{
    public static class ScriptFactory
    {

        #region Public methods

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static ICollection<Domain.Script> Create()
        {
            var config = DatabaseScripterConfig.Instance;

            var scripts = new List<Domain.Script>();

            foreach (var scriptConfig in DatabaseScripterConfig.Instance.Scripts)
            {
                var scriptManifest = config.Manifest.GetManifest(scriptConfig.Name);
                if (scriptManifest == null)
                {
                    log.ErrorFormat(CultureInfo.InvariantCulture, "Could not find script \"{0}\" in the manifest file.");
                    throw new DatabaseScripterException(Domain.ErrorCode.CouldNotFindScript, scriptConfig.Name);
                }
                
                scripts.Add(CreateScript(scriptConfig, scriptManifest));
            }

            return scripts;
        }

        #endregion

        #region Non-public methods

        private static readonly ILog log = LogManager.GetLogger(typeof(ScriptFactory));

        private static Domain.Script CreateScript(Domain.ScriptConfig scriptConfig, Domain.ScriptManifest scriptManifest)
        {
            var command = File.ReadAllText(Path.Combine(Path.GetDirectoryName(DatabaseScripterConfig.Instance.Manifest.FilePath), scriptManifest.Path));

            return new Domain.Script(scriptConfig.Name,
                scriptManifest.Description,
                ScriptConfigManager.GetConfig(scriptConfig, "databaseName"),
                ScriptConfigManager.GetConnectionString(scriptConfig), scriptManifest.WrapInTransaction,
                (scriptManifest.CurrentVersion == null) ? null : new Domain.Version(scriptManifest.CurrentVersion),
                (scriptManifest.NewVersion == null) ? null : new Domain.Version(scriptManifest.NewVersion),
                ScriptConfigInjector.InjectConfig(command, scriptConfig));
        }

        #endregion

    }
}
