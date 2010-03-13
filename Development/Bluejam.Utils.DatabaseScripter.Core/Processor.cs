using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using Bluejam.Utils.DatabaseScripter.DbAdapter;

namespace Bluejam.Utils.DatabaseScripter.Core
{

    public class Processor
    {

        public void Run()
        {
            var scripts = Scripts.ScriptFactory.Create();
            foreach (var script in scripts)
            {
                if (!script.Run())
                {




                    break;
                }
            }

            System.Console.WriteLine("Done.");
            System.Console.ReadLine();
        }

        public Processor(string[] args)
        {
            ConfigurationManager.GetSection("databaseScripter");
        }

    }
}
