using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{

    public abstract class ScriptConfigurationFactory
    {

        #region Nominal

        public static ScriptConfiguration Create
        {
            get { return new ScriptConfiguration("Create"); }
        }

        public static ScriptConfiguration IncrementTo0_0_0_1
        {
            get { return new ScriptConfiguration("Increment to 0.0.0.1"); }
        }

        public static ScriptConfiguration InsertSampleData
        {
            get
            {
                var scriptConfiguration = new ScriptConfiguration("insert sample data");
                scriptConfiguration.Properties.AddRange(PropertyCollectionFactory.InsertSampleData);

                return scriptConfiguration;
            }
        }

        #endregion

        #region Other

        public static ScriptConfiguration NullName
        {
            get
            {
                var scriptConfiguration = Create;
                scriptConfiguration.Name = null;

                return scriptConfiguration;
            }
        }

        public static ScriptConfiguration EmptyName
        {
            get
            {
                var scriptConfiguration = Create;
                scriptConfiguration.Name = string.Empty;

                return scriptConfiguration;
            }
        }

        public static ScriptConfiguration WhiteSpaceName
        {
            get
            {
                var scriptConfiguration = Create;
                scriptConfiguration.Name = "  ";

                return scriptConfiguration;
            }
        }

        public static ScriptConfiguration EmptyPropertyCollection
        {
            get { return new ScriptConfiguration("Empty Script"); }
        }

        public static ScriptConfiguration InvalidPropertyInCollection
        {
            get
            {
                var scriptConfiguration = Create;
                scriptConfiguration.Name = "Invalid";
                scriptConfiguration.Properties.Add(PropertyFactory.NullName);

                return scriptConfiguration;
            }
        }

        #endregion

    }
}
