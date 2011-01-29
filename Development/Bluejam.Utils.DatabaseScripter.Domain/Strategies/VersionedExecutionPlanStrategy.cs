﻿//DatabaseScripter  Copyright (C) 2011  Philip Wood
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

namespace Bluejam.Utils.DatabaseScripter.Domain.Strategies
{
    public class VersionedExecutionPlanStrategy
    {
        public Domain.Values.ExecutionPlan Run(Domain.Values.Configuration configuration)
        {
            var executionPlan = new Domain.Values.ExecutionPlan();            

            executionPlan.Environment = configuration.Environment;
            var currentVersion = GetCurrentVersion(configuration);
            var scriptManifests = configuration.Manifest.GetConcurrentScripts(currentVersion, configuration.TargetVersion);
            if (scriptManifests.Count == 0)
            {
                throw new Domain.DatabaseScripterException(Domain.ErrorCode.NoExplicitUpgradePath, string.Format(CultureInfo.InvariantCulture, "Could not find an explicit upgrade path from {0} to {1}", currentVersion, configuration.TargetVersion));
            }
            executionPlan.NameOfScriptsToRun.AddRange(scriptManifests.Select(item => item.Name));
            executionPlan.DatabaseAdapter = Factories.AdapterFactory.Create(configuration.Preview);

            return executionPlan;
        }

        private Values.Version GetCurrentVersion(Domain.Values.Configuration configuration)
        {
            var environmentConfig = configuration.EnvironmentConfigurations.Find(configuration.Environment);
            var databaseName = environmentConfig.Properties.Find("databaseName").Value;
            var connectionString = configuration.ConnectionStrings[environmentConfig.Properties.Find("connection").Value].ConnectionString;
            Values.Version currentVersion;

            using (var databaseAdapter = Factories.AdapterFactory.Create(false))
            {
                if (!databaseAdapter.Initialize())
                {
                    throw new DatabaseScripterException(ErrorCode.FailedToCreateExecutionPlan, "Failed to initialise the database adapter when getting current version");
                }
                if (!databaseAdapter.Connect(connectionString))
                {
                    throw new DatabaseScripterException(ErrorCode.FailedToCreateExecutionPlan, "Failed to connect to the database when getting current version");
                }
                if (!databaseAdapter.GetVersion(databaseName, out currentVersion))
                {
                    throw new DatabaseScripterException(ErrorCode.FailedToCreateExecutionPlan, "Failed to get the current version of the database");
                }
            }

            return currentVersion;
        }
    }
}
