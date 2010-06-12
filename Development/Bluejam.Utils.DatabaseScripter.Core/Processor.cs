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
using System.Globalization;
using System.Linq;
using log4net;


namespace Bluejam.Utils.DatabaseScripter.Core
{

    public sealed class Processor
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Processor));

        public static ErrorCode Run(string[] args)
        {
            try
            {
                Console.WriteLine(License.Splash);

                if (args.ToList().Exists(arg => string.Equals(arg, "-manifestschema", StringComparison.OrdinalIgnoreCase)))
                {
                    var manifestValidator = new Config.ManifestValidator();
                    Console.WriteLine(manifestValidator.SchemaString);
                    return ErrorCode.Ok;
                }

                Config.ConfigurationFactory.Create(args);

                var adapter = Scripts.AdapterFactory.Create();
                adapter.Initialize();

                var scripts = Scripts.ScriptFactory.Create();
                foreach (var script in scripts)
                {
                    var errorCode = script.Run(adapter);
                    if (ErrorCode.Ok != errorCode)
                    {
                        log.ErrorFormat(CultureInfo.InvariantCulture, "Script \"{0}\" failed; subsequent scripts will not run.", script.Name);
                        return errorCode;
                    }
                }
            }
            catch (DatabaseScripterException ex)
            {
                log.Error("An error occurred. Check the debug information that follows.", ex);
                return ex.ErrorCode;
            }

            return ErrorCode.Ok;            
        }

        private Processor()
        {
        }

    }
}
