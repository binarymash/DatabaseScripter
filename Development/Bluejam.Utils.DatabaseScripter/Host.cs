using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;

using Bluejam.Utils.DatabaseScripter.Services.Interfaces;
using Bluejam.Utils.DatabaseScripter.Domain.Interfaces;

namespace Bluejam.Utils.DatabaseScripter
{

    public class Host
    {

        #region Non-public 

        private static readonly ILog log = LogManager.GetLogger(typeof(Host));        

        private IConfigService configService;
        private ILicenceService licenceService;
        private IScriptingService scriptingService;

        #endregion

        #region Constructors

        public Host(IConfigService configService, ILicenceService licenceService, IScriptingService scriptingService)
        {
            this.configService = configService;
            this.licenceService = licenceService;
            this.scriptingService = scriptingService;
        }

        #endregion

        #region  Public methods

        public ErrorCode Run(string[] args)
        {
            ErrorCode errorCode = ErrorCode.Ok;

            try
            {
                Console.WriteLine(licenceService.LicenceSplash);

                var configResult = configService.GetConfiguration(args);
                errorCode = configResult.ErrorCode;
                var configuration = configResult.Configuration;

                if (errorCode == ErrorCode.Ok)
                {
                    if (configuration.Pause)
                    {
                        System.Console.WriteLine("Paused - press any key to run execution plan...");
                        System.Console.ReadLine();
                    }

                    if (configuration.ManifestSchema)
                    {
                        Console.WriteLine(configService.ManifestSchema);
                        return ErrorCode.Ok;
                    }

                    if (configuration.EnvironmentConfigSchema)
                    {
                        Console.WriteLine(configService.EnvironmentConfigSchema);
                        return ErrorCode.Ok;
                    }

                    if (configuration.ConfigSchema)
                    {
                        Console.WriteLine(configService.ConfigSchema);
                        return ErrorCode.Ok;
                    }
                }

                IExecutionPlanResult executionPlanResult = null;
                if (errorCode == ErrorCode.Ok)
                {
                    executionPlanResult = scriptingService.GetExecutionPlan(configuration);
                    errorCode = executionPlanResult.ErrorCode;
                }

                if (errorCode == ErrorCode.Ok)
                {
                    errorCode = scriptingService.Execute(configResult.Configuration, executionPlanResult.ExecutionPlan);
                }
            }
            catch (DatabaseScripterException ex)
            {
                log.Error("An unexpected error occurred", ex);
                errorCode = ex.ErrorCode;
            }

            return errorCode;
        }

        #endregion

    }
}
