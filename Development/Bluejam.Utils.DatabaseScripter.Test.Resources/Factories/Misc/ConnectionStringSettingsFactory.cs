using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Misc
{

    public abstract class ConnectionStringSettingsFactory
    {

        public static ConnectionStringSettings Development
        {
            get 
            {
                return new ConnectionStringSettings()
                {
                    ConnectionString = "MyDevelopmentConnectionString",
                    Name = "Development"
                };
            }
        }

        public static ConnectionStringSettings QA
        {
            get
            {
                return new ConnectionStringSettings()
                {
                    ConnectionString = "MyQAConnectionString",
                    Name = "QA"
                };
            }
        }

    }

}
