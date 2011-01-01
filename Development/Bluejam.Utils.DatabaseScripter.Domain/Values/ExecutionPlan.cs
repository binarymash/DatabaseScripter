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
using System.ComponentModel.DataAnnotations;

using NHibernate.Validator.Constraints;
using Bluejam.Utils.DatabaseScripter.Domain.Validators;

namespace Bluejam.Utils.DatabaseScripter.Domain.Values
{
    public class ExecutionPlan : Core.DomainModel.ValidatableValueObject
    {

        public ExecutionPlan()
        {
            NameOfScriptsToRun = new List<string> { };
        }

        [Valid]
        [NotNull(Message = "The database adapter has not been specified")]
        public Domain.Strategies.DatabaseAdapter DatabaseAdapter { get; set; }

        [NotNullNotWhiteSpace(Message = "The environment has not been specified")]
        public string Environment { get; set; }

        [NotNullNotEmpty(Message = "No scripts have been specified")]
        public List<string> NameOfScriptsToRun { get; private set; }

    }
}
