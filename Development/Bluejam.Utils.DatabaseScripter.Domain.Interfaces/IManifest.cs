using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Domain.Interfaces
{
    public interface IManifest
    {
        IVersion LatestVersion { get; }
        List<IScriptManifest> ScriptManifests { get; set; }
        IScriptManifest GetManifest(string name);
        string FilePath { get; set; }
        List<IScriptManifest> GetConcurrentScripts(IVersion startVersion, IVersion endVersion);
    }
}
