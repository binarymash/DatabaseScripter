using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bluejam.Utils.DatabaseScripter.Domain;
using Factories = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values
{
    public abstract class ConfigurationFactory
    {
        public static Bluejam.Utils.DatabaseScripter.Domain.Values.Configuration Development
        {
            get
            {
                var configuration = new Bluejam.Utils.DatabaseScripter.Domain.Values.Configuration()
                {
                    ConnectionStrings = Factories.Misc.ConnectionStringSettingsCollectionFactory.All,
                    Manifest = ManifestFactory.Default
                };

                configuration.EnvironmentConfigurations.AddRange(Factories.Domain.Entities.EnvironmentConfigurationCollectionFactory.All);

                return configuration;
            }
        }

        public static Bluejam.Utils.DatabaseScripter.Domain.Values.Configuration Invalid_MixingNamedAndTargetVersion
        {
            get
            {
                var configuration = Development;
                configuration.NameOfScriptsToRun.AddRange(Factories.Domain.Entities.ScriptCollectionFactory.Nominal.Select(item => item.Name).ToList<string>());
                configuration.TargetVersion = VersionFactory.v0_0_0_2;

                return configuration;
            }
        }

        public static Bluejam.Utils.DatabaseScripter.Domain.Values.Configuration DevelopmentWithOnlyTargetVersion
        {
            get
            {
                var configuration = Development;
                configuration.NameOfScriptsToRun.Clear();
                configuration.TargetVersion = VersionFactory.v0_0_0_2;

                return configuration;
            }
        }

    }
}
