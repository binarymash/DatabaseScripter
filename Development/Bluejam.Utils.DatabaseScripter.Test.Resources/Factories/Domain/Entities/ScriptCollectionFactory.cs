using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{
    public abstract class ScriptCollectionFactory
    {

        public static ScriptCollection Nominal
        {
            get
            {
                var scripts = new ScriptCollection();
                scripts.Add(ScriptFactory.Nominal);
                scripts.Add(ScriptFactory.Nominal2);

                return scripts;
            }
        }

        public static ScriptCollection ContainsDuplicateScriptNames
        {
            get
            {
                var scripts = Nominal;
                scripts.Add(ScriptFactory.DuplicateNominalName);

                return scripts;
            }
        }

    }
}
