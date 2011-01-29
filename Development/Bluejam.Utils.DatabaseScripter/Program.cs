using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Configuration;

using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using Microsoft.Practices.ServiceLocation;

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
                    //test initialisation of IoC components. If any components cannot be found then report an error.
                    var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
                    ServiceLocator.SetLocatorProvider(() => new CommonServiceLocator.WindsorAdapter.WindsorServiceLocator(container));
                }
                catch(System.Exception ex)
                {
                    throw new Domain.DatabaseScripterException(Domain.ErrorCode.FailedToInitialiseComponents, ex.Message);
                }

                var licenseService = new Services.LicenceService();
                var configService = new Services.ConfigService();
                var scriptingService = new Services.ScriptingService();

                Console.WriteLine(licenseService.LicenceSplash);

                var configResult = configService.GetConfiguration(args);
                errorCode = configResult.ErrorCode;
                var configuration = configResult.Configuration;
                if (errorCode == Domain.ErrorCode.Ok)
                {
                    if (configuration.Pause)
                    {
                        System.Console.WriteLine("Paused - press any key to run execution plan...");
                        System.Console.ReadLine();
                    }

                    if (configuration.ManifestSchema)
                    {
                        Console.WriteLine(configService.ManifestSchema);
                        return (int)Domain.ErrorCode.Ok;
                    }

                    if (configuration.EnvironmentConfigSchema)
                    {
                        Console.WriteLine(configService.EnvironmentConfigSchema);
                        return (int)Domain.ErrorCode.Ok;
                    }

                    if (configuration.ConfigSchema)
                    {
                        Console.WriteLine(configService.ConfigSchema);
                        return (int)Domain.ErrorCode.Ok;
                    }
                }

                Services.ExecutionPlanResult executionPlanResult = null;
                if (errorCode == Domain.ErrorCode.Ok)
                {
                    executionPlanResult = scriptingService.GetExecutionPlan(configuration);
                    errorCode = executionPlanResult.ErrorCode;
                }

                if (errorCode == Domain.ErrorCode.Ok)
                {
                    errorCode = scriptingService.Execute(configResult.Configuration, executionPlanResult.ExecutionPlan);
                }
            }
            catch (Domain.DatabaseScripterException ex)
            {
                log.Error("An unexpected error occurred", ex);
                errorCode = ex.ErrorCode;
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
