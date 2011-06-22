using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

namespace Bluejam.Utils.DatabaseScripter.WindsorInstallers.Runtime
{
    public class DomainInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Domain.Interfaces.IConfiguration>().ImplementedBy<Domain.Values.Configuration>());
            container.Register(Component.For<Domain.Interfaces.IEnvironmentConfiguration>().ImplementedBy<Domain.Entities.EnvironmentConfiguration>());
            container.Register(Component.For<Domain.Interfaces.IEnvironmentConfigurationCollection>().ImplementedBy<Domain.Entities.EnvironmentConfigurationCollection>());
            container.Register(Component.For<Domain.Interfaces.IExecutionPlan>().ImplementedBy<Domain.Values.ExecutionPlan>());
            container.Register(Component.For<Domain.Interfaces.IExecutionPlanFactory>().ImplementedBy<Domain.Factories.ExecutionPlanFactory>());
            container.Register(Component.For<Domain.Interfaces.IManifest>().ImplementedBy<Domain.Values.Manifest>());
            container.Register(Component.For<Domain.Interfaces.IPropertyCollection>().ImplementedBy<Domain.Entities.PropertyCollection>());
            container.Register(Component.For<Domain.Interfaces.IPropertyFactory>().ImplementedBy<Domain.Factories.PropertyFactory>());
            container.Register(Component.For<Domain.Interfaces.IScriptManifest>().ImplementedBy<Domain.Entities.ScriptManifest>());
            container.Register(Component.For<Domain.Interfaces.IVersion>().ImplementedBy<Domain.Values.Version>());
            container.Register(Component.For<Domain.Interfaces.IVersionFactory>().ImplementedBy<Domain.Factories.VersionFactory>());
        }
    }
}
