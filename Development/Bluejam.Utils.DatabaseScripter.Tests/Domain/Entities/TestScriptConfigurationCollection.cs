using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Domain.Entities;
using Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Entities
{

    [TestFixture]
    public class TestScriptConfigurationCollection : DomainTestBase
    {

        [Test]
        public void WhenScriptNameParameterIsNull_FindReturnsNull()
        {
            var scriptConfigurations = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.ScriptConfigurationCollectionFactory.Nominal;
            Assert.That(scriptConfigurations.Find(null), Is.Null);
        }

        [Test]
        public void WhenScriptNameParameterIsNotInCollection_FindReturnsNull()
        {
            var scriptConfigurations = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.ScriptConfigurationCollectionFactory.Nominal;
            var scriptName = "This script does not exist";
            Assert.That(scriptConfigurations.Exists(item => item.Name == scriptName), Is.False);

            Assert.That(scriptConfigurations.Find(scriptName), Is.Null);
        }

        [Test]
        public void WhenScriptNameParameterIsInCollection_FindReturnsTheScriptConfiguration()
        {
            var scriptConfigurations = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.ScriptConfigurationCollectionFactory.Nominal;
            var expectedScriptConfiguration = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.ScriptConfigurationFactory.IncrementTo0_0_0_1;
            Assert.That(scriptConfigurations.Exists(item => item.Name == expectedScriptConfiguration.Name));

            var scriptConfiguration = scriptConfigurations.Find(expectedScriptConfiguration.Name);
            Assert.That(scriptConfiguration.Name, Is.EqualTo(expectedScriptConfiguration.Name));
            Assert.That(scriptConfiguration.Properties, Has.Count.EqualTo(expectedScriptConfiguration.Properties.Count));
            foreach (var property in expectedScriptConfiguration.Properties)
            {
                Assert.That(scriptConfiguration.Properties.Exists(item => (item.Name == property.Name) && (item.Value == property.Value)));
            }
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenMembersAreUniqueAndValidTheCollectionIsValid()
        {
            var collection = new ScriptConfigurationCollection()
            {
                ScriptConfigurationFactory.Create,
                ScriptConfigurationFactory.IncrementTo0_0_0_1,
            };

            AssertIsValid(collection);
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenMembersAreNotUniqueTheCollectionIsInvalid()
        {
            var collection = new ScriptConfigurationCollection()
            {
                ScriptConfigurationFactory.Create,
                ScriptConfigurationFactory.Create
            };

            AssertIsInvalid(collection, 1);
            AssertValidationResult(collection, null, "There is more than one script configuration with the same name");
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenMembersAreInvalidTheCollectionIsInvalid()
        {
            var collection = new ScriptConfigurationCollection()
            {
                ScriptConfigurationFactory.InvalidPropertyInCollection
            };

            AssertIsInvalid(collection, 1);
            AssertValidationResult(collection, "Name", "A name must be specified for a property");
        }
    
    }

}
