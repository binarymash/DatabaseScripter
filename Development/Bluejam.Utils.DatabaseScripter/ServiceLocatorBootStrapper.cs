using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

namespace Bluejam.Utils.DatabaseScripter
{
    public static class ServiceLocatorBootStrapper
    {
        public static CommonServiceLocator.WindsorAdapter.WindsorServiceLocator Run()
        {
            var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

            //TODO: wire up through config
            container.Install(
                new HostWindsorInstaller(),
                new WindsorInstallers.Runtime.ServicesInstaller(),
                new WindsorInstallers.Runtime.ConfigInstaller()
                );

            return new CommonServiceLocator.WindsorAdapter.WindsorServiceLocator(container);

        }
    }
}
