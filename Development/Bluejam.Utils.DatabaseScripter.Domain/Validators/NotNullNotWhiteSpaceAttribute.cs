using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Validator.Engine;

namespace Bluejam.Utils.DatabaseScripter.Domain.Validators
{

    [ValidatorClass(typeof(NotNullNotWhiteSpaceValidator)), AttributeUsage(AttributeTargets.Property)]
    public sealed class NotNullNotWhiteSpaceAttribute : Attribute, IRuleArgs
    {
        private string message = "May not be null or whitespace";

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
