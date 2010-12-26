using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{
    public abstract class EnvironmentConfigurationCollectionFactory
    {

        #region Nominal

        public static EnvironmentConfigurationCollection All
        {
            get
            {
                return new EnvironmentConfigurationCollection()
                {
                     EnvironmentConfigurationFactory.Development
                };
            }
        }

        #endregion

    }
}
