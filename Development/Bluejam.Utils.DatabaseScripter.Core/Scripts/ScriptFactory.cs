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
