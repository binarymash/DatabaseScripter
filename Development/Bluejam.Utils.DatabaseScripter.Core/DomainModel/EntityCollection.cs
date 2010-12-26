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

using SharpArch.Core;
using SharpArch.Core.CommonValidator;
using SharpArch.Core.DomainModel;

namespace Bluejam.Utils.DatabaseScripter.Core.DomainModel
{
    public abstract class EntityCollection<T> : List<T>, IValidatable where T : Entity
    {
        public virtual bool IsValid()
        {
            return Validator.IsValid(this);
        }

        public virtual ICollection<IValidationResult> ValidationResults()
        {
            return Validator.ValidationResultsFor(this);
        }

        private IValidator Validator
        {
            get
            {
                return SafeServiceLocator<IValidator>.GetService();
            }
        }

        /// <summary>
        /// This property is only here to allow us to bubble up the validation status of
        /// the members of the collection, as the Valid attribute cannot be set on the 
        /// class itself.
        /// </summary>
        /// <value>The contents.</value>
        //[NHibernate.Validator.Constraints.Valid]
        //protected EntityCollection<T> Contents
        //{
        //    get { return this; }
        //}

    }
}
