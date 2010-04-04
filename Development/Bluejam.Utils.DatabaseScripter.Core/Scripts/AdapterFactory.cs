using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;

using Bluejam.Utils.DatabaseScripter.Core.Config;
using Bluejam.Utils.DatabaseScripter.DbAdapter;

namespace Bluejam.Utils.DatabaseScripter.Core.Scripts
{
    public static class AdapterFactory
    {

        public static IDatabaseAdapter Create()
        {
            var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

            if (DatabaseScripterConfig.Instance.Preview)
            {
                return (IDatabaseAdapter)container["previewAdapter"];
            }

            return (IDatabaseAdapter)container["databaseAdapter"];           
        }
    }
}
