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

using System;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;

using log4net;

namespace Bluejam.Utils.DatabaseScripter.Domain.Entities
{
    [Serializable]
    public class EnvironmentConfigurationCollection : Core.DomainModel.EntityCollection<EnvironmentConfiguration>
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(EnvironmentConfigurationCollection));

        public EnvironmentConfiguration Find(string environmentName)
        {
            return Find(item => string.Equals(item.Name, environmentName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
