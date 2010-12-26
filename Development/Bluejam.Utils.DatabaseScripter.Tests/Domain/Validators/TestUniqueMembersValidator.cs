using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Validators;
using Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Validators
{
    [TestFixture]
    public class TestUniqueMembersValidator : DomainTestBase
    {

        [Test]
        public void ThrowsExceptionWhenNotCollection()
        {
            var validator = new UniqueMembersValidator();

            try
            {
                int i = 10;
                Assert.That(validator.IsValid(i, null), Is.False);
                Assert.Fail();
            }
            catch (SharpArch.Core.PreconditionException)
            {
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ReturnsTrueWhenMembersAreUnique()
        {
            var validator = new UniqueMembersValidator();
            var collection = PropertyCollectionFactory.InsertSampleData;
            Assert.That(validator.IsValid(collection, null), Is.True);
        }

        [Test]
        public void ReturnsFalseWhenMembersAreNotUnique()
        {
            var validator = new UniqueMembersValidator();
            var collection = PropertyCollectionFactory.ContainsDuplicatePropertyNames;
            Assert.That(validator.IsValid(collection, null), Is.False);
        }

    }
}
