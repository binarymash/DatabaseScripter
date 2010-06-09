using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public static class ConfigurationFactory
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ConfigurationFactory));

        public static void Create(string[] args)
        {
            try
            {
                //set config from app.config
                ConfigurationManager.GetSection("databaseScripter");

                //overridden command line switches
                foreach (var arg in args)
                {
                    if (string.Equals(arg, "-preview", StringComparison.OrdinalIgnoreCase))
                    {
                        DatabaseScripterConfig.Instance.Preview = true;
                    }
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                log.Error("An unexpected error occurred when reading the configuration", ex);
                if (ex.InnerException != null)
                {
                    if (ex.InnerException is DatabaseScripterException)
                    {
                        //we do this so that we can get a better error code to return to the user
                        throw ex.InnerException;
                    }
                }
                else
                {
                    throw new DatabaseScripterException(ErrorCode.InvalidConfig, "The configuration contains errors.", ex);
                }
            }
        }
    }
}
