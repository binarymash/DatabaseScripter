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

namespace Bluejam.Utils.DatabaseScripter.Config
{
    public class ManifestFactoryResult : Interfaces.Result
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestFactoryResult"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="manifest">The manifest.</param>
        public ManifestFactoryResult(Domain.Interfaces.ErrorCode errorCode, Domain.Values.Manifest manifest)
            : base(errorCode)
        {
            Manifest = manifest;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the manifest.
        /// </summary>
        /// <value>The manifest.</value>
        public Domain.Values.Manifest Manifest { get; protected set; }

        #endregion


    }
}
