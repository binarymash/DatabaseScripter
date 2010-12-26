using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bluejam.Utils.DatabaseScripter.Test.Resources.Factories.Domain.Entities;

using NUnit.Framework;

namespace Bluejam.Utils.DatabaseScripter.Tests.Domain.Entities
{
    [TestFixture]
    public class TestEnvironmentConfiguration : DomainTestBase
    {

        #region Constructors

        [Test]
        public void DefaultConstructor()
        {
            var environmentConfiguration = new Bluejam.Utils.DatabaseScripter.Domain.Entities.EnvironmentConfiguration();
            Assert.That(environmentConfiguration.Name, Is.Null);
            Assert.That(environmentConfiguration.ScriptConfigurations, Is.Empty);
            Assert.That(environmentConfiguration.Properties, Is.Empty);
        }

        #endregion

        [Test]
        public void NominalIsValid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.Development;
            AssertIsValid(environmentConfiguration);
        }

        #region GetFlatConfigurationForScript

        [Test]
        public void WhenScriptNameParameterIsNull_GetFlatConfigurationForScriptThrowsInvalidArgumentException()
        {
            try
            {
                var environmentConfiguration = EnvironmentConfigurationFactory.Development;
                var scriptConfiguration = environmentConfiguration.GetFlatConfigurationForScript(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.That(ex.ParamName, Is.EqualTo("scriptName"));
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void WhenScriptNameParameterIsNotInScriptConfigurations_GetFlatConfigurationForScriptReturnsGlobalProperties()
        {
            var scriptName = "This script does not exist";
            var environmentConfiguration = EnvironmentConfigurationFactory.Development;
            Assert.That(environmentConfiguration.ScriptConfigurations.Exists(config => config.Name == scriptName), Is.False);

            var scriptConfiguration = environmentConfiguration.GetFlatConfigurationForScript(scriptName);
            Assert.That(scriptConfiguration.Name, Is.EqualTo(scriptName));
            Assert.That(scriptConfiguration.Properties, Has.Count.EqualTo(environmentConfiguration.Properties.Count));
            foreach(var property in environmentConfiguration.Properties)
            {
                Assert.That(scriptConfiguration.Properties.Exists(item => (item.Name == property.Name) && (item.Value == property.Value)));
            }
        }

        [Test]
        public void WhenScriptNameParameterIsInScriptConfigurations_GetFlatConfigurationForScriptReturnsScriptPropertiesOverGlobalProperties()
        {
            var scriptManifest = ScriptManifestFactory.InsertSampleData;
            var environmentConfiguration = EnvironmentConfigurationFactory.Development;

            environmentConfiguration.Properties.Clear();
            environmentConfiguration.Properties.Add(new Bluejam.Utils.DatabaseScripter.Domain.Entities.Property("myName1", "myGlobalValue1"));
            environmentConfiguration.Properties.Add(new Bluejam.Utils.DatabaseScripter.Domain.Entities.Property("myName2", "myGlobalValue2"));
            environmentConfiguration.Properties.Add(new Bluejam.Utils.DatabaseScripter.Domain.Entities.Property("myName3", "myGlobalValue3"));
            
            var scriptConfiguration = environmentConfiguration.ScriptConfigurations.Find(item => item.Name == scriptManifest.Name);
            scriptConfiguration.Properties.Clear();
            scriptConfiguration.Properties.Add(new Bluejam.Utils.DatabaseScripter.Domain.Entities.Property("myName3", "myScriptValue3"));
            scriptConfiguration.Properties.Add(new Bluejam.Utils.DatabaseScripter.Domain.Entities.Property("myName4", "myScriptValue4"));

            var flatConfiguration = environmentConfiguration.GetFlatConfigurationForScript(scriptManifest.Name);

            Assert.That(flatConfiguration.Name, Is.EqualTo(scriptManifest.Name));
            Assert.That(flatConfiguration.Properties, Has.Count.EqualTo(4));
            Assert.That(flatConfiguration.Properties.Exists(item => item.Name == "myName1" && item.Value == "myGlobalValue1"));
            Assert.That(flatConfiguration.Properties.Exists(item => item.Name == "myName2" && item.Value == "myGlobalValue2"));
            Assert.That(flatConfiguration.Properties.Exists(item => item.Name == "myName3" && item.Value == "myScriptValue3"));
            Assert.That(flatConfiguration.Properties.Exists(item => item.Name == "myName4" && item.Value == "myScriptValue4"));
        }

        #endregion

        #region Name

        [Test]
        public void WhenNameIsNullTheEnvironmentConfigurationIsInvalid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.NullName;
            AssertIsInvalid(environmentConfiguration, 1);
            AssertValidationResult(environmentConfiguration.ValidationResults(), "Name", "A name must be specified for an environment configuration");
        }

        [Test]
        public void WhenNameIsEmptyTheEnvironmentConfigurationIsInvalid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.EmptyName;
            AssertIsInvalid(environmentConfiguration, 1);
            AssertValidationResult(environmentConfiguration.ValidationResults(), "Name", "A name must be specified for an environment configuration");
        }

        [Test]
        public void WhenNameIsWhiteSpaceTheEnvironmentConfigurationIsInvalid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.WhiteSpaceName;
            AssertIsInvalid(environmentConfiguration, 1);
            AssertValidationResult(environmentConfiguration.ValidationResults(), "Name", "A name must be specified for an environment configuration");
        }

        #endregion

        #region Properties

        [Test]
        public void WhenPropertiesIsEmptyTheEnvironmentConfigurationIsValid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.EmptyProperties;

        }

        [Test]
        public void WhenPropertiesHasInvalidMemberTheEnvironmentConfigurationIsInvalid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.InvalidProperties;
            AssertIsInvalid(environmentConfiguration, 1);
            AssertValidationResult(environmentConfiguration.ValidationResults(), "Name", "A name must be specified for a property");
        }

        [Test]
        public void WhenPropertiesHasDuplicateMembersTheEnvironmentConfigurationIsInvalid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.Development;
            environmentConfiguration.Properties.Add(PropertyFactory.Description);
            environmentConfiguration.Properties.Add(PropertyFactory.Description);

            AssertIsInvalid(environmentConfiguration, 1);
            AssertValidationResult(environmentConfiguration.ValidationResults(), "Properties", "There is more than one property with the same name");
        }

        #endregion

        #region ScriptConfiguration

        [Test]
        public void WhenScriptConfigurationsIsEmptyTheEnvironmentConfigurationIsValid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.Development;
            environmentConfiguration.ScriptConfigurations.Clear();
            AssertIsValid(environmentConfiguration);
        }

        [Test]
        public void WhenScriptConfigurationsHasInvalidMemberTheEnvironmentConfigurationIsInvalid()
        {
            var environmentConfiguration = EnvironmentConfigurationFactory.InvalidScriptConfiguration;
            AssertIsInvalid(environmentConfiguration, 1);
            AssertValidationResult(environmentConfiguration.ValidationResults(), "Name", "A name must be specified for a property");
        }

        #endregion

    }
}
