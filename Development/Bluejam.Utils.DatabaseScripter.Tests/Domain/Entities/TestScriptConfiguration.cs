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
    public class TestScriptConfiguration : DomainTestBase
    {

        [Test]
        public void WhenNameAndValueAreSet_ThePropertyIsValid()
        {
            AssertIsValid(ScriptConfigurationFactory.InsertSampleData);
        }

        [Test]
        public void WhenNameIsNull_ThePropertyIsInvalid()
        {
            var property = ScriptConfigurationFactory.NullName;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property, "Name", "A name must be supplied for the script configuration");
        }

        [Test]
        public void WhenNameIsEmpty_ThePropertyIsInvalid()
        {
            var property = ScriptConfigurationFactory.EmptyName;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property, "Name", "A name must be supplied for the script configuration");
        }

        [Test]
        public void WhenNameIsWhiteSpace_ThePropertyIsInvalid()
        {
            var property = ScriptConfigurationFactory.WhiteSpaceName;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property, "Name", "A name must be supplied for the script configuration");
        }

        [Test]
        public void WhenPropertyCollectionIsEmpty_TheScriptConfigurationIsValid()
        {
            AssertIsValid(ScriptConfigurationFactory.EmptyPropertyCollection);
        }

        [Test]
        public void WhenPropertyCollectionContainsAnInvalidProperty_TheScriptConfigurationIsInvalid()
        {
            var property = ScriptConfigurationFactory.InvalidPropertyInCollection;

            AssertIsInvalid(property, 1);
            AssertValidationResult(property.ValidationResults(), "Name", "A name must be specified for a property");
        }

    }
}
