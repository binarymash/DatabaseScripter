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
using System.Globalization;
using System.Linq;
using System.Text;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using log4net;

namespace Bluejam.Utils.DatabaseScripter.Services
{
    public class ScriptingService
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(ScriptingService));
        private Domain.ConfigInjector configInjector;

        public ScriptingService()
        {
            var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            configInjector = (Domain.ConfigInjector)container["configInjector"];
        }

        public Domain.ErrorCode Execute(Domain.Configuration configuration, Domain.ExecutionPlan executionPlan)
        {
            try
            {
                var scripts = Domain.ScriptFactory.Create(configuration, executionPlan, configInjector);
                executionPlan.DatabaseAdapter.Initialize();
                foreach(var script in scripts)
                {
                    var errorCode = script.Run(executionPlan.DatabaseAdapter);
                    if (Domain.ErrorCode.Ok != errorCode)
                    {
                        log.ErrorFormat(CultureInfo.InvariantCulture, "Script \"{0}\" failed; subsequent scripts will not run.", script.Name);
                        return errorCode;
                    }
                }

                return Domain.ErrorCode.Ok;
            }
            catch (Domain.DatabaseScripterException ex)
            {
                log.Error("An error occurred. Check the debug information that follows.", ex);
                return ex.ErrorCode;
            }
        }
    }
}
