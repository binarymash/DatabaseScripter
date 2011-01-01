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
using Domain = Bluejam.Utils.DatabaseScripter.Domain;

namespace Bluejam.Utils.DatabaseScripter.Services
{
    public class ConfigurationResult : Result
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationResult"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="executionPlan">The execution plan.</param>
        public ConfigurationResult(Domain.ErrorCode errorCode, Domain.Values.Configuration configuration)
            : base(errorCode)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        public Domain.Values.Configuration Configuration { get; protected set; }

        #endregion


    }
}
