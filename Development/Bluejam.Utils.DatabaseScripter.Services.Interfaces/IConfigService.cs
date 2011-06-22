using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Services.Interfaces
{
    public interface IConfigService
    {

        string ManifestSchema { get; }
        string ConfigSchema { get; }
        string EnvironmentConfigSchema { get; }
        IConfigurationResult GetConfiguration(string[] args);

    }
}
