using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Config.Interfaces
{
    public interface IConfigurationFactory
    {
        Domain.Values.Configuration Create(string[] args);
    }
}
