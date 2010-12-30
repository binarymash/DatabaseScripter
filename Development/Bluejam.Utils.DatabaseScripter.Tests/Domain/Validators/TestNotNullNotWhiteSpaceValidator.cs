using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Validators;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Validators
{
    [TestFixture]
    public class TestNotNullNotWhiteSpaceValidator : DomainTestBase
    {

        [Test]
        public void ThrowsExceptionWhenNotString()
        {
            var validator = new NotNullNotWhiteSpaceValidator();

            try
            {
                int i = 10;
                Assert.That(validator.IsValid(i, null), Is.False);
                Assert.Fail();
            }
            catch (SharpArch.Core.PreconditionException)
            {
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ReturnsFalseWhenNull()
        {
            var validator = new NotNullNotWhiteSpaceValidator();

            Assert.That(validator.IsValid((string)null, null), Is.False);
        }

        [Test]
        public void ReturnsTrueWhenNoWhiteSpace()
        {
            var validator = new NotNullNotWhiteSpaceValidator();

            Assert.That(validator.IsValid("abc", null), Is.True);
        }

        [Test]
        public void ReturnsTrueWhenPartialWhiteSpace()
        {
            var validator = new NotNullNotWhiteSpaceValidator();

            Assert.That(validator.IsValid(" this is ok ", null), Is.True);
        }

        [Test]
        public void ReturnsFalseWhenWhiteSpace()
        {
            var validator = new NotNullNotWhiteSpaceValidator();

            Assert.That(validator.IsValid(" ", null), Is.False);
        }

        [Test]
        public void ReturnsFalseWhenTabbedWhiteSpace()
        {
            var validator = new NotNullNotWhiteSpaceValidator();

            Assert.That(validator.IsValid(Convert.ToChar(9).ToString(), null), Is.False);
        }

    }
}
