//DatabaseScripter  Copyright (C) 2010  Philip Wood
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public static class ExecutionPlanFactory
    {

        public static Domain.ExecutionPlan Create(IEnumerable<string> args)
        {
            var executionPlan = new Domain.ExecutionPlan();

            foreach (var arg in args)
            {
                if (arg.StartsWith("--environment=", StringComparison.OrdinalIgnoreCase) || 
                    arg.StartsWith("-e=", StringComparison.OrdinalIgnoreCase))
                {
                    executionPlan.Environment = GetArgumentValue(arg);
                }

                if (arg.StartsWith("--scripts=", StringComparison.OrdinalIgnoreCase) || 
                    arg.StartsWith("-s=", StringComparison.OrdinalIgnoreCase))
                {
                    executionPlan.NameOfScriptsToRun = GetArgumentValue(arg).Split(',').ToList();
                }
            }

            executionPlan.DatabaseAdapter = AdapterFactory.Create(args.ToList().Exists(arg => string.Equals(arg, "--preview", StringComparison.OrdinalIgnoreCase)));

            return executionPlan;
        }

        private static string GetArgumentValue(string arg)
        {
            var equalsPos = arg.IndexOf('=');
            if (equalsPos < 0)
            {
                throw new ArgumentException("Not a valid argument", arg);
            }

            return arg.Substring(equalsPos + 1, arg.Length - (equalsPos + 1));
        }

    }
}
