using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Config.Interfaces
{
    public interface ICommandLineReader
    {
        Domain.Values.Configuration Interpret(List<string> arguments, Domain.Values.Configuration configuration);
    }
}
