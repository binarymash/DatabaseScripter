using System;
using System.Collections;
using System.Linq;
using System.Text;

using NHibernate.Validator.Engine;
using SharpArch.Core;
using SharpArch.Core.DomainModel;

namespace Bluejam.Utils.DatabaseScripter.Domain.Validators
{
    public class UniqueMembersValidator: IValidator
    {
        private const string constraintMessage = "This validator may only be used on an ICollection containing BaseObject objects";

        public bool IsValid(object value, IConstraintValidatorContext constraintValidatorContext)
        {
            var collection = value as ICollection;           
            Check.Require(collection != null, constraintMessage);

            foreach (var member in collection)
            {
                Check.Require(member is BaseObject, constraintMessage);
            }

            foreach (var member in collection)
            {
                var enumerator = collection.GetEnumerator();
                while(enumerator.MoveNext())
                {
                    if ((member != enumerator.Current) &&
                       ((BaseObject)member).HasSameObjectSignatureAs((BaseObject)enumerator.Current))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
