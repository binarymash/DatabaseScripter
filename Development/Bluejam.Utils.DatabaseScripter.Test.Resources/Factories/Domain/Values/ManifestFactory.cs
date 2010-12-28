using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bluejam.Utils.DatabaseScripter.Domain;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Values
{
    public abstract class ManifestFactory
    {
        public static Bluejam.Utils.DatabaseScripter.Domain.Values.Manifest Empty
        {
            get
            {
                var manifest = new Bluejam.Utils.DatabaseScripter.Domain.Values.Manifest();
                manifest.FilePath = @"c:\some\path\manifest.xml";
                manifest.ScriptManifests = new List<Bluejam.Utils.DatabaseScripter.Domain.Entities.ScriptManifest>();

                return manifest;
            }
        }

        public static Bluejam.Utils.DatabaseScripter.Domain.Values.Manifest Basic
        {
            get
            {
                var manifest = Empty;

                manifest.ScriptManifests.Add(Domain.Entities.ScriptManifestFactory.Script1);
                manifest.ScriptManifests.Add(Domain.Entities.ScriptManifestFactory.Script2);

                return manifest;
            }
        }

        public static Bluejam.Utils.DatabaseScripter.Domain.Values.Manifest Default
        {
            get
            {
                var manifest = Empty;

                manifest.ScriptManifests.Add(Domain.Entities.ScriptManifestFactory.Create);
                manifest.ScriptManifests.Add(Domain.Entities.ScriptManifestFactory.Drop);
                manifest.ScriptManifests.Add(Domain.Entities.ScriptManifestFactory.IncrementTo0_0_0_1);
                manifest.ScriptManifests.Add(Domain.Entities.ScriptManifestFactory.IncrementTo0_0_0_2); 
                manifest.ScriptManifests.Add(Domain.Entities.ScriptManifestFactory.InsertSampleData);

                return manifest;
            }
        }

    }
}
