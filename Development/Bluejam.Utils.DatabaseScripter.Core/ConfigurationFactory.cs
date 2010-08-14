//DatabaseScripter  Copyright (C) 2010  Philip Wood
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Core
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
                    throw new DatabaseScripterException(Domain.ErrorCode.InvalidConfig, "The configuration contains errors.", ex);
                }
            }
        }
    }
}
