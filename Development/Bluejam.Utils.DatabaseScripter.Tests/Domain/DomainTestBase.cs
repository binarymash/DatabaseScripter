using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Core.CommonValidator;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain
{
    public class DomainTestBase : TestBase
    {

        public void AssertIsValid(IValidatable obj)
        {
            Assert.That(obj.IsValid(), Is.True);
            Assert.That(obj.ValidationResults(), Has.Count.EqualTo(0));
        }

        public void AssertIsInvalid(IValidatable obj, int expectedErrorCount)
        {
            Assert.That(obj.IsValid(), Is.False);
            Assert.That(obj.ValidationResults(), Has.Count.EqualTo(expectedErrorCount));
        }

        public void AssertValidationResult(ICollection<SharpArch.Core.CommonValidator.IValidationResult> validationResults, string expectedName, string expectedMessage)
        {
            Assert.That(validationResults.ToList().Exists(item => ((item.PropertyName == expectedName) && (item.Message == expectedMessage))));
        }

        public void AssertValidationResult(IValidatable obj, string expectedName, string expectedMessage)
        {
            AssertValidationResult(obj.ValidationResults(), expectedName, expectedMessage);
        }

    }
}
