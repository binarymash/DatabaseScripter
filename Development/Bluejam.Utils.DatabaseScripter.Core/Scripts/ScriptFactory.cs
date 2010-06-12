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
using log4net;


namespace Bluejam.Utils.DatabaseScripter.Core.Scripts
{
    public static class ScriptFactory
    {

        #region Public methods

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static ICollection<Script> Create()
        {
            var config = Config.DatabaseScripterConfig.Instance;

            var scripts = new List<Script>();

            foreach (var scriptConfig in Config.DatabaseScripterConfig.Instance.Scripts)
            {
                var scriptManifest = config.Manifest.GetManifest(scriptConfig.Name);
                if (scriptManifest == null)
                {
                    log.ErrorFormat(CultureInfo.InvariantCulture, "Could not find script \"{0}\" in the manifest file.");
                    throw new DatabaseScripterException(ErrorCode.CouldNotFindScript, scriptConfig.Name);
                }
                
                scripts.Add(CreateScript(scriptConfig, scriptManifest));
            }

            return scripts;
        }

        #endregion

        #region Non-public methods

        private static readonly ILog log = LogManager.GetLogger(typeof(ScriptFactory));

        private static Script CreateScript(Config.ScriptConfig scriptConfig, Config.ScriptManifest scriptManifest)
        {
            return new Script(scriptConfig, scriptManifest);
        }

        #endregion

    }
}
