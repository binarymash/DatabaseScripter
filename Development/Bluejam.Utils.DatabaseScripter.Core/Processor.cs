using System;
using System.Globalization;
using System.Linq;
using log4net;


namespace Bluejam.Utils.DatabaseScripter.Core
{

    public sealed class Processor
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Processor));

        public static ErrorCode Run(string[] args)
        {
            try
            {

                if (args.ToList().Exists(arg => string.Equals(arg, "-manifestschema", StringComparison.OrdinalIgnoreCase)))
                {
                    var manifestValidator = new Config.ManifestValidator();
                    Console.WriteLine(manifestValidator.SchemaString);
                    return ErrorCode.Ok;
                }

                Config.ConfigurationFactory.Create(args);

                var adapter = Scripts.AdapterFactory.Create();
                adapter.Initialize();

                var scripts = Scripts.ScriptFactory.Create();
                foreach (var script in scripts)
                {
                    var errorCode = script.Run(adapter);
                    if (ErrorCode.Ok != errorCode)
                    {
                        log.ErrorFormat(CultureInfo.InvariantCulture, "Script \"{0}\" failed; subsequent scripts will not run.", script.Name);
                        return errorCode;
                    }
                }
            }
            catch (DatabaseScripterException ex)
            {
                log.Error("An error occurred. Check the debug information that follows.", ex);
                return ex.ErrorCode;
            }

            return ErrorCode.Ok;            
        }

        private Processor()
        {
        }

    }
}
