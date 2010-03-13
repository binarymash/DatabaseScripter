using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Bluejam.Utils.DatabaseScripter
{
    class Program
    {
        static void Main(string[] args)
        {
            new Core.Processor(args).Run();
        }

    }
}
