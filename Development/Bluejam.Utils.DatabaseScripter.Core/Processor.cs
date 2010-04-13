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
            Config.ConfigurationFactory.Create(args);

            var adapter = Scripts.AdapterFactory.Create();
            adapter.Initialize();

            var errorCode = ErrorCode.Ok;
            var scripts = Scripts.ScriptFactory.Create();
            foreach (var script in scripts)
            {
                var result = script.Run(adapter);
                if (result != ErrorCode.Ok)
                {
                    break;
                }
            }

            System.Console.WriteLine("Done.");
            return errorCode;
        }

        private Processor()
        {
        }

    }
}
