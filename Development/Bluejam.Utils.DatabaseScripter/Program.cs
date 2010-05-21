using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;

namespace Bluejam.Utils.DatabaseScripter
{
    class Program
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            var errorCode = Core.Processor.Run(args);
            if (errorCode == Core.ErrorCode.Ok)
            {
                log.InfoFormat(CultureInfo.InvariantCulture, "The database scripter exited with code {0} ({1})", (int)errorCode, errorCode);
            }
            else
            {
                log.ErrorFormat(CultureInfo.InvariantCulture, "The database scripter exited with error code {0} ({1})", (int)errorCode, errorCode);
            }
        }

    }
}
