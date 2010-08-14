using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Configuration;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;
using Services = Bluejam.Utils.DatabaseScripter.Services;

using log4net;

namespace Bluejam.Utils.DatabaseScripter
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        private static Services.LicenceService licenseService = new Services.LicenceService();
        private static Services.ConfigService configService = new Services.ConfigService();
        private static Services.ScriptingService scriptingService = new Services.ScriptingService();

        static int Main(string[] args)
        {
            Console.WriteLine(licenseService.LicenceSplash);
            Domain.ErrorCode errorCode = Domain.ErrorCode.Ok;

            if (args.ToList().Exists(arg => string.Equals(arg, "-manifestschema", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(configService.ManifestSchema);
                return (int)Domain.ErrorCode.Ok;
            }

            if (args.ToList().Exists(arg => string.Equals(arg, "-configschema", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(configService.ConfigSchema);
                return (int)Domain.ErrorCode.Ok;
            }

            errorCode = configService.Create(args);
            if (errorCode == Domain.ErrorCode.Ok)
            {
                errorCode = scriptingService.Execute();
            }

            if (errorCode == Domain.ErrorCode.Ok)
            {
                log.InfoFormat(CultureInfo.InvariantCulture, "The database scripter exited with code {0} ({1})", (int)errorCode, errorCode);
            }
            else
            {
                log.ErrorFormat(CultureInfo.InvariantCulture, "The database scripter exited with error code {0} ({1})", (int)errorCode, errorCode);
            }

            return (int)errorCode;
        }

    }
}
