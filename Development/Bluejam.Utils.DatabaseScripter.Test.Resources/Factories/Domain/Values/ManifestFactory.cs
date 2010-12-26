using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bluejam.Utils.DatabaseScripter.Domain;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values
{
    public abstract class ManifestFactory
    {
        public static Bluejam.Utils.DatabaseScripter.Domain.Values.Manifest Default
        {
            get
            {
                var manifest = new Bluejam.Utils.DatabaseScripter.Domain.Values.Manifest();
                manifest.FilePath = @"c:\some\path\manifest.xml";
                manifest.ScriptManifests = new List<Bluejam.Utils.DatabaseScripter.Domain.Entities.ScriptManifest>();

                var scriptManifest = new Bluejam.Utils.DatabaseScripter.Domain.Entities.ScriptManifest();
                scriptManifest.Name = "script1";
                scriptManifest.Path = @"c:\some\path\script1.sql";
                scriptManifest.WrapInTransaction = true;
                manifest.ScriptManifests.Add(scriptManifest);

                scriptManifest = new Bluejam.Utils.DatabaseScripter.Domain.Entities.ScriptManifest();
                scriptManifest.Name = "script2";
                scriptManifest.Path = @"c:\some\path\script2.sql";
                scriptManifest.Description = "This is a description of script 2";
                scriptManifest.CurrentVersion = "0.0.0.0";
                scriptManifest.NewVersion = "0.0.0.1";
                manifest.ScriptManifests.Add(scriptManifest);

                return manifest;
            }
        }

    }
}
