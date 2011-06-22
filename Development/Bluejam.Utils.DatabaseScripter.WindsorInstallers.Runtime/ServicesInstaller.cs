using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Services = Bluejam.Utils.DatabaseScripter.Services;

namespace Bluejam.Utils.DatabaseScripter.WindsorInstallers.Runtime
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Services.Interfaces.IConfigService>().ImplementedBy<Services.ConfigService>(),
                Component.For<Services.Interfaces.ILicenceService>().ImplementedBy<Services.LicenceService>(),
                Component.For<Services.Interfaces.IScriptingService>().ImplementedBy<Services.ScriptingService>()
            );
        }
    }
}
