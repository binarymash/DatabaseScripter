using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using Bluejam.Utils.DatabaseScripter.DbAdapter;

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
                    throw new DatabaseScripterException(ErrorCode.CouldNotFindScript, scriptConfig.Name);
                }

                var versionedScriptManifest = scriptManifest as Config.VersionedScriptManifest;
                if (versionedScriptManifest != null)
                {
                    scripts.Add(CreateVersionedScript(scriptConfig, versionedScriptManifest));
                    continue;
                }
                
                scripts.Add(CreateScript(scriptConfig, scriptManifest));
            }

            return scripts;
        }

        #endregion

        #region Non-public methods

        private static Script CreateScript(Config.ScriptConfig scriptConfig, Config.ScriptManifest scriptManifest)
        {
            return new Script(scriptConfig, scriptManifest);
        }

        private static VersionedScript CreateVersionedScript(Config.ScriptConfig scriptConfig, Config.VersionedScriptManifest scriptManifest)
        {
            return new VersionedScript(scriptConfig, scriptManifest);
        }

        #endregion

    }
}
