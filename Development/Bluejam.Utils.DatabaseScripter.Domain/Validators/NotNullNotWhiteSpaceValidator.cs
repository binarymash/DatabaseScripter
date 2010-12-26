using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Validator.Engine;
using SharpArch.Core;

namespace Bluejam.Utils.DatabaseScripter.Domain.Validators
{
    public class NotNullNotWhiteSpaceValidator: IValidator
    {

        public bool IsValid(object value, IConstraintValidatorContext constraintValidatorContext)
        {
            if (value == null)
            {
                return false;
            }

            Check.Require(value is string, "This validator may only be used on a String");
            return (!string.IsNullOrEmpty((value as string).Trim()));
        }
    }
}
