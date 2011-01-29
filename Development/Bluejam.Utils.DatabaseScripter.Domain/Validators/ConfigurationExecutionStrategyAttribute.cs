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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Validator.Engine;

using SharpArch.Core.DomainModel;

namespace Bluejam.Utils.DatabaseScripter.Domain.Validators
{

    [ValidatorClass(typeof(ConfigurationExecutionStrategyValidator)), AttributeUsage(AttributeTargets.Class)]
    public sealed class ConfigurationExecutionStrategyAttribute : Attribute, IRuleArgs
    {
        private string message = "The configuration execution strategy is invalid; it must be either named or versioned";

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    } 
}
