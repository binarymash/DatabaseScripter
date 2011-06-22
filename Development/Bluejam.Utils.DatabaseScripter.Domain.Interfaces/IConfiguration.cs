using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Domain.Interfaces
{
    public interface IConfiguration
    {
        System.Configuration.ConnectionStringSettingsCollection ConnectionStrings { get; }
        IEnvironmentConfigurationCollection EnvironmentConfigurations { get; }
        IManifest Manifest { get; set; }
        IVersion TargetVersion { get; set; }
        bool Pause { get; set; }
        bool ManifestSchema { get; set; }
        bool EnvironmentConfigSchema { get; set; }
        bool ConfigSchema { get; set; }
        string Environment { get; set; }
        List<string> NameOfScriptsToRun { get; }
        bool Preview { get; set; }

        bool IsValid();

    }
}
