using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{
    public abstract class ScriptConfigurationCollectionFactory
    {

        #region Nominal

        public static ScriptConfigurationCollection Nominal
        {
            get
            {
                var collection = new ScriptConfigurationCollection();
                collection.Add(ScriptConfigurationFactory.Create);
                collection.Add(ScriptConfigurationFactory.IncrementTo0_0_0_1);
                collection.Add(ScriptConfigurationFactory.InsertSampleData);

                return collection;
            }
        }

        #endregion

        #region Other

        public static ScriptConfigurationCollection Empty
        {
            get
            {
                return new ScriptConfigurationCollection();
            }
        }

        #endregion

    }
}
