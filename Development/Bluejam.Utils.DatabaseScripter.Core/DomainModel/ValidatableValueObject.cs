using System;
using System.Collections.Generic;
using SharpArch.Core;
using SharpArch.Core.DomainModel;
using SharpArch.Core.CommonValidator;

namespace Bluejam.Utils.DatabaseScripter.Core.DomainModel
{
    [Serializable]
    public abstract class ValidatableValueObject : ValueObject, IValidatable
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
    }
}
