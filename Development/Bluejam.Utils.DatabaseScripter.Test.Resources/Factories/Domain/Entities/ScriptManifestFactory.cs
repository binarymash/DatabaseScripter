using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;

namespace Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities
{

    public abstract class ScriptManifestFactory
    {

        public static ScriptManifest Create
        {
            get
            {
                return new ScriptManifest()
                {
                    Name = "create",
                    Description = "Creates the database.",
                    Path = @"scripts\create.sql",
                    CurrentVersion = null,
                    NewVersion = "0.0.0.0",
                    WrapInTransaction = false
                };
            }
        }

        public static ScriptManifest Drop
        {
            get
            {
                return new ScriptManifest()
                {
                    Name = "drop",
                    Description = "Drops the database",
                    Path = @"scripts\drop.sql",
                    CurrentVersion = null,
                    NewVersion = null,
                    WrapInTransaction = false
                };
            }
        }

        public static ScriptManifest IncrementTo0_0_0_1
        {
            get
            {
                return new ScriptManifest()
                {
                    Name = "increment to 0.0.0.1",
                    Description = "Initialises the schema.",
                    Path = @"scripts\0.0.0.1.sql",
                    CurrentVersion = "0.0.0.0",
                    NewVersion = "0.0.0.1",
                    WrapInTransaction = false
                };
            }
        }

        public static ScriptManifest IncrementTo0_0_0_2
        {
            get
            {
                return new ScriptManifest()
                {
                    Name = "increment to 0.0.0.2",
                    Description = "Changes the schema.",
                    Path = @"scripts\0.0.0.2.sql",
                    CurrentVersion = "0.0.0.1",
                    NewVersion = "0.0.0.2",
                    WrapInTransaction = false
                };
            }
        }

        public static ScriptManifest InsertSampleData
        {
            get
            {
                return new ScriptManifest()
                {
                    Name = "insert sample data",
                    Description = "Inserts sample data.",
                    Path = @"scripts\sampleData.sql",
                    CurrentVersion = "0.0.0.1",
                    NewVersion = null,
                    WrapInTransaction = false
                };
            }
        }

        public static ScriptManifest Script1
        {
            get
            {
                var scriptManifest = new Bluejam.Utils.DatabaseScripter.Domain.Entities.ScriptManifest();
                scriptManifest.Name = "script1";
                scriptManifest.Path = @"c:\some\path\script1.sql";
                scriptManifest.WrapInTransaction = true;

                return scriptManifest;
            }
        }

        public static ScriptManifest Script2
        {
            get
            {
                var scriptManifest = new Bluejam.Utils.DatabaseScripter.Domain.Entities.ScriptManifest();
                scriptManifest.Name = "script2";
                scriptManifest.Path = @"c:\some\path\script2.sql";
                scriptManifest.Description = "This is a description of script 2";
                scriptManifest.CurrentVersion = "0.0.0.0";
                scriptManifest.NewVersion = "0.0.0.1";

                return scriptManifest;
            }
        }
    }

}
