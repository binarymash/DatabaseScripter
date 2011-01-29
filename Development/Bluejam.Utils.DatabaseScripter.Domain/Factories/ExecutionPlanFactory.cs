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

namespace Bluejam.Utils.DatabaseScripter.Domain.Factories
{
    public class ExecutionPlanFactory
    {

        public Domain.Values.ExecutionPlan Create(Domain.Values.Configuration configuration)
        {
            if (!configuration.IsValid())
            {
                throw new DatabaseScripterException(ErrorCode.InvalidConfig);
            }

            if (configuration.NameOfScriptsToRun.Count > 0)
            {
                return new Strategies.NamedExecutionPlanStrategy().Run(configuration);
            }
            else if (configuration.TargetVersion != null)
            {
                return new Strategies.VersionedExecutionPlanStrategy().Run(configuration);
            }

            //TODO: latest strategy

            return null;

        }
    }
}
