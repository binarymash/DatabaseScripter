using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain;
using Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Entities
{

    [TestFixture]
    public class TestProperty : DomainTestBase
    {

        [Test]
        public void EmptyConstructor()
        {
            var property = new Bluejam.Utils.DatabaseScripter.Domain.Entities.Property();
            Assert.That(property.Name, Is.Null);
            Assert.That(property.Value, Is.Null);
        }

        [Test]
        public void ParameterisedConstructor()
        {
            var property = new Bluejam.Utils.DatabaseScripter.Domain.Entities.Property("MyName", "MyProperty");
            Assert.That(property.Name, Is.EqualTo("MyName"));
            Assert.That(property.Value, Is.EqualTo("MyProperty"));
        }

        [Test]
        public void WhenNameAndValueAreSetThePropertyIsValid()
        {
            AssertIsValid(PropertyFactory.Title);
        }

        [Test]
        public void WhenNameIsNullThePropertyIsInvalid()
        {
            var property = PropertyFactory.NullName;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property.ValidationResults(), "Name", "A name must be specified for a property");
        }

        [Test]
        public void WhenNameIsEmptyThePropertyIsInvalid()
        {
            var property = PropertyFactory.EmptyName;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property.ValidationResults(), "Name", "A name must be specified for a property");
        }

        [Test]
        public void WhenNameIsWhiteSpaceThePropertyIsInvalid()
        {
            var property = PropertyFactory.WhiteSpaceName;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property.ValidationResults(), "Name", "A name must be specified for a property");
        }

        [Test]
        public void WhenValueIsNullThePropertyIsInvalid()
        {
            var property = PropertyFactory.NullValue;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property.ValidationResults(), "Value", "A value must be specified for a property");
        }

        [Test]
        public void WhenValueIsEmptyThePropertyIsValid()
        {
            AssertIsValid(PropertyFactory.EmptyValue);
        }

        [Test]
        public void WhenValueIsWhiteSpaceThePropertyIsValid()
        {
            AssertIsValid(PropertyFactory.WhiteSpaceValue);
        }

        [Test]
        public void TheNameUniquelyIdentifiesAProperty()
        {
            var property1 = PropertyFactory.Description;
            var property2 = PropertyFactory.Description;
            Assert.That(property1.HasSameObjectSignatureAs(property2), Is.True);

            property2.Value = "SomeOtherValue";
            Assert.That(property1.Value, Is.Not.EqualTo(property2.Value));
            Assert.That(property1.HasSameObjectSignatureAs(property2), Is.True);

            property2.Name = "SomeOtherName";
            Assert.That(property1.Name, Is.Not.EqualTo(property2.Name));
            Assert.That(property1.HasSameObjectSignatureAs(property2), Is.False);

            property2.Value = property1.Value;
            Assert.That(property1.HasSameObjectSignatureAs(property2), Is.False);

            property2.Name = property1.Name;
            Assert.That(property1.HasSameObjectSignatureAs(property2), Is.True);
        
        }
    
    }
}
