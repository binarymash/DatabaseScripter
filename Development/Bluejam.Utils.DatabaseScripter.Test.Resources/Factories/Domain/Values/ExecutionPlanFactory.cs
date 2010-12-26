using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bluejam.Utils.DatabaseScripter.Domain;
using Factories = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values
{
    public abstract class ExecutionPlanFactory
    {
        public static Bluejam.Utils.DatabaseScripter.Domain.Values.ExecutionPlan Development
        {
            get
            {
                var executionPlan = new Bluejam.Utils.DatabaseScripter.Domain.Values.ExecutionPlan()
                {
                    DatabaseAdapter = new Bluejam.Utils.DatabaseScripter.DbAdapter.FileWriter.Adapter(),
                    Environment = Factories.Domain.Entities.EnvironmentConfigurationFactory.Development.Name,
                };

                executionPlan.NameOfScriptsToRun.AddRange(Factories.Domain.Entities.ScriptCollectionFactory.Nominal.Select(item => item.Name).ToList<string>());

                return executionPlan;
            }
        }

        public static Bluejam.Utils.DatabaseScripter.Domain.Values.ExecutionPlan EmptyDevelopment
        {
            get
            {
                return new Bluejam.Utils.DatabaseScripter.Domain.Values.ExecutionPlan()
                {
                    DatabaseAdapter = new Bluejam.Utils.DatabaseScripter.DbAdapter.FileWriter.Adapter(),
                    Environment = Factories.Domain.Entities.EnvironmentConfigurationFactory.Development.Name,
                };
            }
        }
    }
}
