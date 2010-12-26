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
using Domain = Bluejam.Utils.DatabaseScripter.Domain;

namespace Bluejam.Utils.DatabaseScripter.Services
{
    public class ExecutionPlanResult : Result
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionPlanResult"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="executionPlan">The execution plan.</param>
        public ExecutionPlanResult(Domain.ErrorCode errorCode, Domain.Values.ExecutionPlan executionPlan)
            : base(errorCode)
        {
            ExecutionPlan = executionPlan;
        }

        #endregion

        #region Properties

        public Domain.Values.ExecutionPlan ExecutionPlan { get; protected set; }

        #endregion


    }
}
