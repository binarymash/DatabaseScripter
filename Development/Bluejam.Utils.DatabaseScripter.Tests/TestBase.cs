using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.ServiceLocation;

using SharpArch.Core.CommonValidator;

using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests
{
    public class TestBase
    {

        [SetUp]
        public virtual void SetUp()
        {
            var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            ServiceLocator.SetLocatorProvider(() => new CommonServiceLocator.WindsorAdapter.WindsorServiceLocator(container));
        }

    }
}
