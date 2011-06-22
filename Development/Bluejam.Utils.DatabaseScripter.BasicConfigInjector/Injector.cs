//DatabaseScripter  Copyright (C) 2011  Philip Wood
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

using System.Globalization;
using System.Text.RegularExpressions;

using Domain = Bluejam.Utils.DatabaseScripter.Domain;

using log4net;


namespace Bluejam.Utils.DatabaseScripter.BasicConfigInjector
{
    public class Injector : Domain.Strategies.ConfigInjector
    {
        public override string InjectConfig(string command, Domain.Entities.PropertyCollection properties)
        {
            var regex = new Regex(@"\x24\x7B[a-zA-z0-9\-_]+\x7D");
            var match = regex.Match(command);
            while (match.Success)
            {
                var token = match.Value.Substring(2, match.Value.Length - 3);
                var property = properties.Find(token);
                if (property == null)
                {
                    throw new Domain.Interfaces.DatabaseScripterException(Domain.Interfaces.ErrorCode.CouldNotFindPropertyForScriptInEnvironmentConfiguration, "token");
                }

                command = command.Replace(match.Value, property.Value);
                log.DebugFormat(CultureInfo.InvariantCulture, "Injected configuration key {0} with value {1}", match.Value, property.Value);

                match = regex.Match(command);
            }

            return command;
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(Injector));

    }
}
