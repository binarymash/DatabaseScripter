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

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public class ExecutionPlanFactory
    {

        public Domain.Values.ExecutionPlan Create(Domain.Values.Configuration configuration)
        {
            var executionPlan = new Domain.Values.ExecutionPlan();
            executionPlan.Environment = configuration.Environment;
            if (configuration.NameOfScriptsToRun.Count > 0)
            {
                executionPlan.NameOfScriptsToRun.AddRange(configuration.NameOfScriptsToRun);
            }
            else if (configuration.TargetVersion != null && configuration.CurrentVersion != null)
            {
                var scriptManifests = configuration.Manifest.GetConcurrentScripts(configuration.CurrentVersion, configuration.TargetVersion);
                if (scriptManifests.Count == 0)
                {
                    throw new Domain.DatabaseScripterException(Domain.ErrorCode.NoExplicitUpgradePath, string.Format(CultureInfo.InvariantCulture, "Could not find an explicit upgrade path from {0} to {1}", configuration.CurrentVersion, configuration.TargetVersion));
                }
                executionPlan.NameOfScriptsToRun.AddRange(scriptManifests.Select(item => item.Name));
            }
            executionPlan.DatabaseAdapter = AdapterFactory.Create(configuration.Preview);

            return executionPlan;
        }

    }
}
