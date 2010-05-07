using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using Bluejam.Utils.DatabaseScripter.DbAdapter;

namespace Bluejam.Utils.DatabaseScripter.Core
{

    public sealed class Processor
    {

        public static ErrorCode Run(string[] args)
        {
            try
            {
                Config.ConfigurationFactory.Create(args);

                var adapter = Scripts.AdapterFactory.Create();
                adapter.Initialize();

                var scripts = Scripts.ScriptFactory.Create();
                foreach (var script in scripts)
                {
                    script.Run(adapter);
                }

                System.Console.WriteLine("Done.");
                return ErrorCode.Ok;
            }
            catch (DatabaseScripterException ex)
            {
                return ex.ErrorCode;
            }
        }

        private Processor()
        {
        }

    }
}
