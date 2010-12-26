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

    }
}
