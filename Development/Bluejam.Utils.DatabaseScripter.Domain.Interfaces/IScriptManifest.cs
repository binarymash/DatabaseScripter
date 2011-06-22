using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Domain.Interfaces
{
    public interface IScriptManifest
    {
        string Name { get; set; }
        string Description { get; set; }
        string Path { get; set; }
        bool WrapInTransaction { get; set; }
        string CurrentVersion { get; set; }
        string NewVersion { get; set; }
    }
}
