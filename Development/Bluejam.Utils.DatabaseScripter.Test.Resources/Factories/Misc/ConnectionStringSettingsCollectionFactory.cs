using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Misc
{

    public abstract class ConnectionStringSettingsCollectionFactory
    {

        public static ConnectionStringSettingsCollection All
        {
            get 
            {
                return new ConnectionStringSettingsCollection()
                {
                    ConnectionStringSettingsFactory.Development,
                    ConnectionStringSettingsFactory.QA
                };
            }
        }


    }

}
