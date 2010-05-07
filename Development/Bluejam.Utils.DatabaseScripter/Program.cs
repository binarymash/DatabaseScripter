using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Bluejam.Utils.DatabaseScripter
{
    class Program
    {

        static void Main(string[] args)
        {
            var errorCode = Core.Processor.Run(args);
            if (errorCode != Core.ErrorCode.Ok)
            {
                Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "The database scripter failed with error code {0} ({1}). Hit return to continue.", (int)errorCode, errorCode));
                Console.ReadLine();
            }
        }

    }
}
