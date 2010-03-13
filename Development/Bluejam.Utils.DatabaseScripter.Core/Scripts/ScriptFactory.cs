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
        public static List<Script> Create()
        {
            var config = Config.Configuration.Instance;

            var scripts = new List<Script>();

            foreach (var scriptConfig in Config.Configuration.Instance.Scripts)
            {
                var scriptManifest = config.Manifest.GetManifest(scriptConfig.Name);
                if (scriptManifest == null)
                {
                    //TODO: log script not found in manifest
                }

                if (scriptManifest is Manifest.VersionedScriptManifest)
                {
                    scripts.Add(CreateVersionedScript(scriptConfig, scriptManifest as Manifest.VersionedScriptManifest));
                    continue;
                }
                
                scripts.Add(CreateScript(scriptConfig, scriptManifest));
            }

            return scripts;
        }

        #endregion

        #region Non-public methods

        private static Script CreateScript(Config.ScriptConfig scriptConfig, Manifest.ScriptManifest scriptManifest)
        {
            return new Script(scriptConfig, scriptManifest);
        }

        private static VersionedScript CreateVersionedScript(Config.ScriptConfig scriptConfig, Manifest.VersionedScriptManifest scriptManifest)
        {
            return new VersionedScript(scriptConfig, scriptManifest);
        }

        #endregion

    }
}
