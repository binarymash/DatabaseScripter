using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Configuration;

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
            var errorCode = InitialiseServiceLocator();

            if (errorCode == Bluejam.Utils.DatabaseScripter.Domain.Interfaces.ErrorCode.Ok)
            {
                errorCode = ServiceLocator.Current.GetInstance<Host>().Run(args);
            }

            if (errorCode == Domain.Interfaces.ErrorCode.Ok)
            {
                log.InfoFormat(CultureInfo.InvariantCulture, "The database scripter exited with code {0} ({1})", (int)errorCode, errorCode);
            }
            else
            {
                log.ErrorFormat(CultureInfo.InvariantCulture, "The database scripter exited with error code {0} ({1})", (int)errorCode, errorCode);
            }

            return (int)errorCode;
        }

        private static Domain.Interfaces.ErrorCode InitialiseServiceLocator()
        {
            try
            {
                ServiceLocator.SetLocatorProvider(() => ServiceLocatorBootStrapper.Run());
            }
            catch (System.Exception ex)
            {
                log.ErrorFormat(CultureInfo.InvariantCulture, "Failed to initialise the database scripter components: {0} ", ex.Message);
                return Domain.Interfaces.ErrorCode.FailedToInitialiseComponents;
            }

            return Domain.Interfaces.ErrorCode.Ok;
        }

    }
}
