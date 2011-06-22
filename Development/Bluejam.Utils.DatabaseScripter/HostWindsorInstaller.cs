using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bluejam.Utils.DatabaseScripter
{
    public class HostWindsorInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Host>());
        }

    }
}
