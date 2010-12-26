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
    public class TestEnvironmentConfigurationCollection : DomainTestBase
    {

        [Test]
        public void WhenEnvironmentNameParameterIsNull_FindReturnsNull()
        {
            var environmentConfigurations = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.EnvironmentConfigurationCollectionFactory.All;
            Assert.That(environmentConfigurations.Find(null), Is.Null);
        }

        [Test]
        public void WhenEnvironmentNameParameterIsNotInCollection_FindReturnsNull()
        {
            var environmentConfigurations = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.EnvironmentConfigurationCollectionFactory.All;
            var environmentName = "This environment does not exist";
            Assert.That(environmentConfigurations.Exists(item => item.Name == environmentName), Is.False);

            Assert.That(environmentConfigurations.Find(environmentName), Is.Null);
        }

        [Test]
        public void WhenEnvironmentNameParameterIsInCollection_FindReturnsTheEnvironmentConfiguration()
        {
            var environmentConfigurations = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.EnvironmentConfigurationCollectionFactory.All;
            var expectedEnvironmentConfiguration = Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities.EnvironmentConfigurationFactory.Development;
            Assert.That(environmentConfigurations.Exists(item => item.Name == expectedEnvironmentConfiguration.Name));

            var environmentConfiguration = environmentConfigurations.Find(expectedEnvironmentConfiguration.Name);
            Assert.That(environmentConfiguration.Name, Is.EqualTo(expectedEnvironmentConfiguration.Name));
            Assert.That(environmentConfiguration.Properties, Has.Count.EqualTo(expectedEnvironmentConfiguration.Properties.Count));
            foreach (var property in expectedEnvironmentConfiguration.Properties)
            {
                Assert.That(environmentConfiguration.Properties.Exists(item => (item.Name == property.Name) && (item.Value == property.Value)));
            }
            Assert.That(environmentConfiguration.ScriptConfigurations, Has.Count.EqualTo(expectedEnvironmentConfiguration.ScriptConfigurations.Count));
            foreach (var scriptConfiguration in expectedEnvironmentConfiguration.ScriptConfigurations)
            {
                Assert.That(environmentConfiguration.ScriptConfigurations.Exists(item => (item.Name == scriptConfiguration.Name)));
            }
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenEmptyTheCollectionIsInvalid()
        {
            var collection = new EnvironmentConfigurationCollection();
            Assert.That(collection.IsValid(), Is.False);
            AssertValidationResult(collection, null, "There are no environment configurations");
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenMembersAreUniqueAndValidTheCollectionIsValid()
        {
            var collection = new EnvironmentConfigurationCollection()
            {
                EnvironmentConfigurationFactory.Development,
            };

            AssertIsValid(collection);
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenMembersAreNotUniqueTheCollectionIsInvalid()
        {
            var collection = new EnvironmentConfigurationCollection()
            {
                EnvironmentConfigurationFactory.Development,
                EnvironmentConfigurationFactory.Development
            };

            AssertIsInvalid(collection, 1);
            AssertValidationResult(collection, null, "There is more than one environment configuration with the same name");
        }

        [Test]
        [Ignore("NHV does not support collection validation - see http://216.121.112.228/browse/NHV-103")]
        public void WhenMembersAreInvalidTheCollectionIsInvalid()
        {
            var collection = new EnvironmentConfigurationCollection()
            {
                EnvironmentConfigurationFactory.InvalidProperties
            };

            AssertIsInvalid(collection, 1);
            AssertValidationResult(collection, "Name", "A name must be specified for a property");
        }
    
    }

}
