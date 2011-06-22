using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public class CommandLineReader : Interfaces.ICommandLineReader
    {
        public Domain.Values.Configuration Interpret(List<string> arguments, Domain.Values.Configuration configuration)
        {
            configuration.Pause = Argument(arguments, "--pause");
            configuration.ManifestSchema = Argument(arguments, "--manifestschema");
            configuration.EnvironmentConfigSchema = Argument(arguments, "--environmentconfigschema");
            configuration.ConfigSchema = Argument(arguments, "--configschema");
            configuration.Preview = Argument(arguments, "--preview");
            configuration.Environment = ArgumentValue(arguments, new List<string>{"--environment", "-e"});
            var scriptNames = ArgumentValue(arguments, new List<string> { "--scripts", "-s" });
            if (scriptNames != null)
            {
                configuration.NameOfScriptsToRun.AddRange(scriptNames.Split(',').ToList());
            }
            var targetVersion = ArgumentValue(arguments, new List<string> { "--target", "-t" });
            if (targetVersion != null)
            {
                configuration.TargetVersion = new Domain.Values.Version(targetVersion);
            }
            return configuration;
        }

        private bool Argument(List<string> arguments, string name)
        {
            return (arguments.Exists(arg => string.Equals(arg, name, StringComparison.OrdinalIgnoreCase)));
        }

        private static string ArgumentValue(List<string> arguments, List<string> names)
        {
            string argument = null;
            foreach (var name in names)
            {
                argument = arguments.Find(arg => (arg.StartsWith(name+"=", StringComparison.OrdinalIgnoreCase)));
                if (argument != null)
                {
                    var equalsPos = argument.IndexOf('=');
                    if (equalsPos < 0)
                    {
                        throw new ArgumentException("Not a valid argument", argument);
                    }

                    return argument.Substring(equalsPos + 1, argument.Length - (equalsPos + 1));
                }
            }

            return null;
        }

    }
}
