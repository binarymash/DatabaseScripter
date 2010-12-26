using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{
    public abstract class EnvironmentConfigurationFactory
    {

        #region Nominal

        public static EnvironmentConfiguration Development
        {
            get
            {
                var environmentConfiguration = new EnvironmentConfiguration();
                environmentConfiguration.Name = "Development";
                environmentConfiguration.Properties.AddRange(PropertyCollectionFactory.NominalGlobal);
                environmentConfiguration.ScriptConfigurations.AddRange(ScriptConfigurationCollectionFactory.Nominal);

                return environmentConfiguration;
            }

        }

        public static EnvironmentConfiguration EmptyProperties
        {
            get
            {
                var environmentConfiguration = Development;
                environmentConfiguration.Properties.Clear();

                return environmentConfiguration;
            }
        }

        #endregion

        #region Other

        public static EnvironmentConfiguration NullName
        {
            get
            {
                var environmentConfiguration = Development;
                environmentConfiguration.Name = null;

                return environmentConfiguration;
            }
        }

        public static EnvironmentConfiguration EmptyName
        {
            get
            {
                var environmentConfiguration = Development;
                environmentConfiguration.Name = string.Empty;

                return environmentConfiguration;
            }
        }


        public static EnvironmentConfiguration WhiteSpaceName
        {
            get
            {
                var environmentConfiguration = Development;
                environmentConfiguration.Name = "   ";

                return environmentConfiguration;
            }
        }

        public static EnvironmentConfiguration InvalidProperties
        {
            get
            {
                var environmentConfiguration = Development;
                environmentConfiguration.Properties.Add(PropertyFactory.NullName);

                return environmentConfiguration;
            }
        }

        public static EnvironmentConfiguration InvalidScriptConfiguration
        {
            get
            {
                var environmentConfiguration = Development;
                environmentConfiguration.ScriptConfigurations.Add(ScriptConfigurationFactory.InvalidPropertyInCollection);

                return environmentConfiguration;
            }
        }

        #endregion

    }
}
