using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Configuration;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;
using Services = Bluejam.Utils.DatabaseScripter.Services;

using log4net;

namespace Bluejam.Utils.DatabaseScripter
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static int Main(string[] args)
        {
            Domain.ErrorCode errorCode = Domain.ErrorCode.Ok;

            try
            {
                try
                {
                    //test initialisation of IoC components
                    new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
                }
                catch(System.Exception ex)
                {
                    throw new Domain.DatabaseScripterException(Domain.ErrorCode.FailedToInitialiseComponents, ex.Message);
                }

                var licenseService = new Services.LicenceService();
                var configService = new Services.ConfigService();
                var scriptingService = new Services.ScriptingService();

                Console.WriteLine(licenseService.LicenceSplash);

                if (args.ToList().Exists(arg => string.Equals(arg, "--pause", StringComparison.OrdinalIgnoreCase)))
                {
                    System.Console.WriteLine("Paused - press any key to run execution plan...");
                    System.Console.ReadLine();
                }

                if (args.ToList().Exists(arg => string.Equals(arg, "--manifestschema", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine(configService.ManifestSchema);
                    return (int)Domain.ErrorCode.Ok;
                }

                if (args.ToList().Exists(arg => string.Equals(arg, "--environmentconfigschema", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine(configService.EnvironmentConfigSchema);
                    return (int)Domain.ErrorCode.Ok;
                }

                if (args.ToList().Exists(arg => string.Equals(arg, "--configschema", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine(configService.ConfigSchema);
                    return (int)Domain.ErrorCode.Ok;
                }

                var configResult = configService.Create(args);
                errorCode = configResult.ErrorCode;

                if (configResult.ErrorCode == Domain.ErrorCode.Ok)
                {
                    errorCode = scriptingService.Execute(configResult.Configuration, configResult.ExecutionPlan);
                }
            }
            catch (Domain.DatabaseScripterException ex)
            {
                return (int)ex.ErrorCode;
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
