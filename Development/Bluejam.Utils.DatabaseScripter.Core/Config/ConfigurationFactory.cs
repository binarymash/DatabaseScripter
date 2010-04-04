using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Bluejam.Utils.DatabaseScripter.Core.Config
{
    public static class ConfigurationFactory
    {

        public static void Create(string[] args)
        {
            //set config from sapp.config
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
    }
}
