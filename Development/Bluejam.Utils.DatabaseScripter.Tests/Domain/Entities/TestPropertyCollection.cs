using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Entities
{
    [TestFixture]
    public class TestPropertyCollection : TestBase
    {

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenAllMembersAreUniqueAndValidTheCollectionIsValid()
        {
            var properties = PropertyCollectionFactory.InsertSampleData;
            Assert.That(properties.IsValid(), Is.True);
            Assert.That(properties.ValidationResults(), Has.Count.EqualTo(0));
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenEmptyTheCollectionIsValid()
        {
            var properties = PropertyCollectionFactory.Empty;
            Assert.That(properties.IsValid(), Is.True);
            Assert.That(properties.ValidationResults(), Has.Count.EqualTo(0));
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenAMemberIsInvalidTheCollectionIsInvalid()
        {
            var properties = PropertyCollectionFactory.ContainsInvalidProperty;
            Assert.That(properties.IsValid(), Is.False);
            Assert.That(properties.ValidationResults(), Has.Count.EqualTo(1));
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenTwoValidPropertiesHaveTheSameNameTheCollectionIsInvalid()
        {
            var properties = PropertyCollectionFactory.ContainsDuplicatePropertyNames;
            Assert.That(properties.IsValid(), Is.False);
            Assert.That(properties.ValidationResults(), Has.Count.EqualTo(1));
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenTwoValidPropertiesHaveTheSameValueTheCollectionIsValid()
        {
            var properties = PropertyCollectionFactory.ContainsDuplicatePropertyValues;
            Assert.That(properties.IsValid(), Is.True);
            Assert.That(properties.ValidationResults(), Has.Count.EqualTo(0));
        }

    }
}
