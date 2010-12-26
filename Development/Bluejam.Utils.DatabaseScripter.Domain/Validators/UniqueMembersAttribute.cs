using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Validator.Engine;

using SharpArch.Core.DomainModel;

namespace Bluejam.Utils.DatabaseScripter.Domain.Validators
{

    [ValidatorClass(typeof(UniqueMembersValidator)), AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
    public sealed class UniqueMembersAttribute : Attribute, IRuleArgs
    {
        private string message = "The collection has duplicate members";

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    } 
}
