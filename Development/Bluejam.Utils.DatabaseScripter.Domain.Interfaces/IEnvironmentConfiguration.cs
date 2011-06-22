using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Domain.Interfaces
{
    public interface IEnvironmentConfiguration
    {
        string Name { get; set; }
        IPropertyCollection Properties { get; set; }
        IScriptConfigurationCollection ScriptConfigurations { get; set; }
        IScriptConfiguration GetFlatConfigurationForScript(string scriptName);
    }
}
