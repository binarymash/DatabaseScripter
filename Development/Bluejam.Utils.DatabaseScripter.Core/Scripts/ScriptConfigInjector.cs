using System.Globalization;
using System.Text.RegularExpressions;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Core.Scripts
{
    public static class ScriptConfigInjector
    {
        public static string InjectConfig(string command, Config.ScriptConfig config)
        {
            var regex = new Regex(@"\x7B[a-zA-z0-9\-_]+\x7D");
            var match = regex.Match(command);
            while(match.Success)
            {
                var injection = ScriptConfigManager.GetConfig(config, match.Value.Substring(1, match.Value.Length - 2));
                command = command.Replace(match.Value, injection);
                log.DebugFormat(CultureInfo.InvariantCulture, "Injected configuration key {0} with value {1}", match.Value, injection);

                match = regex.Match(command);
            }

            return command;
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(ScriptConfigInjector));

    }
}
