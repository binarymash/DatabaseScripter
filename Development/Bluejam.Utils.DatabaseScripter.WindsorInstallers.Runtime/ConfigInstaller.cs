using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Config = Bluejam.Utils.DatabaseScripter.Config;

namespace Bluejam.Utils.DatabaseScripter.WindsorInstallers.Runtime
{
    public class ConfigInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Config.Interfaces.ICommandLineReader>().ImplementedBy<Config.CommandLineReader>());
            container.Register(Component.For<Config.Interfaces.IConfigurationFactory>().ImplementedBy<Config.ConfigurationFactory>());
            container.Register(Component.For<Config.Interfaces.IConfigurationValidator>().ImplementedBy<Config.ConfigurationValidator>());
            container.Register(Component.For<Config.Interfaces.IEnvironmentConfigurationValidator>().ImplementedBy<Config.EnvironmentConfigurationValidator>());
            container.Register(Component.For<Config.Interfaces.IManifestValidator>().ImplementedBy<Config.ManifestValidator>());
        }
    }
}
